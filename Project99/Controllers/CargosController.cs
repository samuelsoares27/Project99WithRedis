using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Project99.Caching;
using Project99.Data;
using Project99.Model;
using Project99.Repository;

namespace Project99.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly ICachingServices _cachingServices;
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "images"); 

        public CargosController(ICargoRepository cargoRepository, ICachingServices cachingServices)
        {
            _cargoRepository = cargoRepository;
            _cachingServices = cachingServices;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetCargos()
        {            
            var lista = await _cargoRepository.GetAllAsync();

            if (lista == null)
            {
                return NotFound();
            }

            return lista;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id)
        {
            Cargo cargo;

            var CargoCache = await _cachingServices.GetAsync(id.ToString());
            
            if (!string.IsNullOrEmpty(CargoCache)) 
            {                
                return Ok(JsonConvert.DeserializeObject<Cargo>(CargoCache));
            }
            else
            {
                cargo = await _cargoRepository.GetByIdAsync(id);

                if (cargo == null)
                {
                    return NotFound("Cargo não cadastrado");
                }
            }

            await _cachingServices.SetAsync(id.ToString(), JsonConvert.SerializeObject(cargo));

            return cargo;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cargo>> PutCargo(int id, Cargo cargo)
        {
            var cargoBase = await _cargoRepository.GetByIdAsync(id);

            if (cargoBase == null)
            {
                return NotFound();
            }

            var cargoUpdate = await _cargoRepository.Update(cargo);

            return cargoUpdate;
        }

        [HttpPost]
        public async Task<ActionResult<Cargo>> PostCargo(Cargo cargo)
        {
            var listaCargo = await _cargoRepository.GetAllAsync();

            if (listaCargo == null)
            {
                return NotFound();
            }

            if (listaCargo.Where(x => x.Nome == cargo.Nome).Any())
            {
                return Problem("Cargo ja existente!");
            }

            var cargoInsert = await _cargoRepository.Insert(cargo);

            return Ok($"O cargo {cargoInsert.Nome} foi inserido com Sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            var cargo = await _cargoRepository.GetByIdAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            await _cargoRepository.Delete(cargo);

            return Ok();
        }

        [HttpPost("imagemCargo")]
        public IActionResult Index(IFormFile image)
        {
            try
            {
                
                if (image == null) 
                    return NotFound("Imagem não carregada");

                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                using (var stream = new FileStream(Path.Combine(_path, image.FileName), FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                using (var webPFileStream = new FileStream(Path.Combine(_path, Guid.NewGuid() + ".webp"), FileMode.Create))
                {
                    using ImageFactory imageFactory = new(preserveExifData: false);
                    imageFactory.Load(image.OpenReadStream())
                                .Format(new WebPFormat())
                                .Quality(100)
                                .Save(webPFileStream);
                }

                return Ok("Imagem salva com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro no upload: " + ex.Message);
            }

        }
    }

}

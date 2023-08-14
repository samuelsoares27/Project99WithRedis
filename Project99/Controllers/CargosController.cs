using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private static string GetCacheKey(int id)
        {
            return $"{string.Join("-", id)}";
        }

    }
}

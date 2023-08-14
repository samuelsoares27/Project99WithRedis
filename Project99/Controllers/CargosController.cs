using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public CargosController(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        // GET: api/Cargos
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

        // GET: api/Cargos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id)
        {
            var cargo = await _cargoRepository.GetByIdAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            return cargo;
        }

        // PUT: api/Cargos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Cargos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/Cargos/5
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

    }
}

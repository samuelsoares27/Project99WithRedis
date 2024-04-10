using Project99.Model;
using Project99.Repository;

namespace Project99.Negocio
{
    public class Negocio
    {            
        private ICargoRepository _cargoRepository;

        public Negocio(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<List<Cargo>> ListarCargos()
        {
            var lista = await _cargoRepository.GetAllAsync();
            return lista;
        }

        public async Task<Cargo> BuscarCargos(int id)
        {
            var cargo = (await _cargoRepository.GetAllAsync()).First(x => x.Id == id);
            return cargo;
        }

    }
}

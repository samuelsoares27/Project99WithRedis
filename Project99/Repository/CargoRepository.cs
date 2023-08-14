using Microsoft.EntityFrameworkCore;
using Project99.Data;
using Project99.Model;

namespace Project99.Repository
{
    public class CargoRepository : Repository<Cargo>, ICargoRepository
    {
        private readonly DataContext _context;
        public CargoRepository(DataContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }
    }
}

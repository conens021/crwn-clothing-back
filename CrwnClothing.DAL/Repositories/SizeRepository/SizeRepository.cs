
using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.SizeRepository
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        private readonly CrwnClothingContext _context;

        public SizeRepository(CrwnClothingContext context) : base(context) 
        {
            _context = context;
        }
    }
}

using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.ColorRepository
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        private readonly CrwnClothingContext _context;

        public ColorRepository(CrwnClothingContext context) : base(context)
        {
            _context = context;
        }
    }
}

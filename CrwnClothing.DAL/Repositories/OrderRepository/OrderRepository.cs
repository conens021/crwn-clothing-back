using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.OrderRepository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly CrwnClothingContext _context;

        public OrderRepository(CrwnClothingContext context) : base(context)
        {
            _context = context;
        }
    }
}

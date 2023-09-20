using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrwnClothing.DAL.Repositories.OrderRepository
{
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        private readonly CrwnClothingContext _context;

        public OrderProductRepository(CrwnClothingContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<OrderProduct> GetByOrderId(int orderId)
        {
            return _context.OrderProducts.AsNoTracking().Where(op => op.OrderId == orderId);
        }
    }
}

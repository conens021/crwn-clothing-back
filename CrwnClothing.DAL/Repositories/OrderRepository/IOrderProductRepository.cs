using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.OrderRepository
{
    public interface IOrderProductRepository : IRepository<OrderProduct>
    {
        public IEnumerable<OrderProduct> GetByOrderId(int orderId);
    }
}

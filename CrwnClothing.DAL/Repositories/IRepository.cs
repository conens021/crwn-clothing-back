
using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Models;
using CrwnClothing.DAL.Models.Sorting;
using System.Linq.Expressions;

namespace CrwnClothing.DAL.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T? GetById(int id);
        Task<T> Create(T entity);
        List<T> CreateBulk(List<T> entities);
        Task<T> Update(T entity);
        List<T> UpdateBulk(List<T> entities);
        Task<T> Delete(T entity);
        List<T> DeleteBulk(List<T> entities);
        IQueryable<T> GetAll(Pagination pagination);
        IQueryable<T> GetAll(Pagination pagination, SortingModel sorting);
    }
}

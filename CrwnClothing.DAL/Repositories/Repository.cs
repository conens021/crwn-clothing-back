using CrwnClothing.DAL.Context;
using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Models;
using CrwnClothing.DAL.Helpers;
using Microsoft.EntityFrameworkCore;
using CrwnClothing.DAL.Models.Sorting;
using System.Linq.Expressions;

namespace CrwnClothing.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly CrwnClothingContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(CrwnClothingContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            entity.CreatedAt = DateTime.Now;

            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();

            return entity;
        }

        public List<T> CreateBulk(List<T> entities)
        {
            entities.ForEach((entity) =>
            {
                entity.CreatedAt = DateTime.Now;
            });

            _context.AddRange(entities);

            _context.SaveChanges();


            return entities;
        }

        public async Task<T> Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }

        public List<T> UpdateBulk(List<T> entities)
        {
            entities.ForEach((entity) =>
            {
                entity.UpdatedAt = DateTime.Now;
            });

            _context.UpdateRange(entities);

            _context.SaveChanges();

            return entities;
        }

        public async Task<T> Delete(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();


            return entity;
        }

        public List<T> DeleteBulk(List<T> entities)
        {
            _context.RemoveRange(entities);

            _context.SaveChanges();

            return entities;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public IQueryable<T> GetAll(Pagination pagination)
        {
            return _dbSet.AsNoTracking().Page(pagination);
        }

        public IQueryable<T> GetAll(Pagination pagination, SortingModel sorting)
        {
            return _dbSet.AsNoTracking()
                    .OrderByPropertyName(sorting.OrderBy, sorting.OrderDirection)
                    .Page(pagination);
        }

        public T? GetById(int id)
        {
            return _dbSet.AsNoTracking().First(e => e.Id == id);
        }
    }
}

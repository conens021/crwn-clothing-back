using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.SortingDto;

namespace CrwnClothing.BLL.Services
{
    public interface IBaseService<T, E>
    {
        List<T> GetAll();
        List<T> GetAll(PaginationDTO paginationDTO);
        List<T> GetAll(PaginationDTO paginationDTO, SortingDTO sorting);
        T? GetById(int id);
        T GetSafeById(int id);
        Task<T> Create(E entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }
}

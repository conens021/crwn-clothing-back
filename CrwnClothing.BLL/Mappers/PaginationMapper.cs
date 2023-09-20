using CrwnClothing.BLL.DTOs;
using CrwnClothing.DAL.Models;

namespace CrwnClothing.BLL.Mappers
{
    public static class PaginationMapper
    {
        public static Pagination ToEntity(this PaginationDTO paginationDTO) => new()
        {
            Page = paginationDTO.Page,  
            PageSize = paginationDTO.PageSize,
        };
    }
}

using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.DAL.Models.Sorting;

namespace CrwnClothing.BLL.Mappers
{
    public static class SortingMapper
    {
        public static SortingModel ToEntity(this SortingDTO sortingDTO) => new()
        {
            OrderBy = sortingDTO.OB,
            OrderDirection = sortingDTO.OD == null ? null : (OrderDirection)sortingDTO.OD
        };
    }
}

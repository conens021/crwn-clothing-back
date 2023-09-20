
using CrwnClothing.BLL.DTOs.SizesDTOs;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers.SizeMappers
{
    public static class SizeMappers
    {
        public static SizeDTO ToDTO(this Size entity) => new() 
        {
            Id = entity.Id,
            Name = entity.Name,
            Value = entity.Value,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };

        public static SizeWithQuantityDTO ToDTOWithQuantity(this ProductsSize entity) => new() 
        {
            Id = entity.Size.Id,
            Name = entity.Size.Name,
            Value = entity.Size.Value,
            QuantityAvailable = entity.QuantityAvailable,
            CreatedAt = entity.Size.CreatedAt,
            UpdatedAt = entity.Size.UpdatedAt
            
        };

        public static Size ToEntity(this SizeDTO dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Value = dto.Value,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };

        public static Size ToEntity(this CreateSizeDTO createDTO) => new()
        {
            Name = createDTO.Name,
            Value = createDTO.Value,
        };
        
    }
}

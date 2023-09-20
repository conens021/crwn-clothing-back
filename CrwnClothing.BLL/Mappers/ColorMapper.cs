using CrwnClothing.BLL.DTOs.ColorDTOs;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers
{
    public static class ColorMapper
    {
        public static Color ToEntity(this ColorDTO dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Value = dto.Value,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };

        public static Color ToEntity(this CreateColorDTO dto) => new()
        {
            Name = dto.Name,
            Value = dto.Value,
        };

        public static Color ToEntity(this CreateColorDTO dto, ColorDTO existing) => new()
        {
            Id = existing.Id,
            Name = dto.Name,
            Value = dto.Value,
            CreatedAt = existing.CreatedAt,
            UpdatedAt = existing.UpdatedAt
        };

        public static ColorDTO ToDTO(this Color entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Value = entity.Value,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}

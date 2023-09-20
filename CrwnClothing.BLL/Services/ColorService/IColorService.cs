using CrwnClothing.BLL.DTOs.ColorDTOs;

namespace CrwnClothing.BLL.Services.ColorService
{
    public interface IColorService : IBaseService<ColorDTO,CreateColorDTO>
    {
        public Task<ColorDTO> Update(int id,CreateColorDTO createColorDTO);
        public Task<ColorDTO> UpdateName(int id, string name);
        public Task<ColorDTO> UpdateValue(int id, string value);
    }
}

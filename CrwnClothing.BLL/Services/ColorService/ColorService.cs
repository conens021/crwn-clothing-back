using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.ColorDTOs;
using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.DAL.Repositories.ColorRepository;
using CrwnClothing.BLL.Mappers;
using CrwnClothing.DAL.Entities;
using DroneDropshipping.BLL.Exceptions;

namespace CrwnClothing.BLL.Services.ColorService
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repository;


        public ColorService(IColorRepository repository)
        {
            _repository = repository;
        }


        public async Task<ColorDTO> Create(CreateColorDTO dto)
        {
            Color created = await _repository.Create(dto.ToEntity());


            return created.ToDTO();
        }

        public async Task<ColorDTO> Delete(ColorDTO dto)
        {
            Color color = await _repository.Delete(dto.ToEntity());


            return color.ToDTO();
        }

        public List<ColorDTO> GetAll()
        {
            return _repository.GetAll().Select(e => e.ToDTO()).ToList();
        }

        public List<ColorDTO> GetAll(PaginationDTO paginationDTO)
        {
            return _repository.GetAll(paginationDTO.ToEntity()).Select(e => e.ToDTO()).ToList();

        }

        public List<ColorDTO> GetAll(PaginationDTO paginationDTO, SortingDTO sorting)
        {
            return _repository.GetAll(paginationDTO.ToEntity(), sorting.ToEntity())
                .Select(e => e.ToDTO()).ToList();
        }

        public ColorDTO? GetById(int id)
        {
            return _repository.GetById(id)?.ToDTO();
        }

        public ColorDTO GetSafeById(int id)
        {
            Color? color = _repository.GetById(id);

            if (color == null) throw new BusinessException("Color not found!", 404);


            return color.ToDTO();
        }

        public async Task<ColorDTO> Update(ColorDTO dto)
        {
            Color color = await _repository.Update(dto.ToEntity());


            return color.ToDTO();
        }

        public async Task<ColorDTO> Update(int id, CreateColorDTO createColorDTO)
        {
            ColorDTO colorDTO = this.GetSafeById(id);

            Color updated = await _repository.Update(createColorDTO.ToEntity(colorDTO));


            return updated.ToDTO();
        }

        public async Task<ColorDTO> UpdateName(int id, string name)
        {
            ColorDTO colorDTO = this.GetSafeById(id);

            colorDTO.Name = name;

            ColorDTO updated = await this.Update(colorDTO);


            return updated;
        }

        public async Task<ColorDTO> UpdateValue(int id, string value)
        {
            ColorDTO colorDTO = this.GetSafeById(id);

            colorDTO.Value = value;

            ColorDTO updated = await this.Update(colorDTO);


            return updated;
        }
    }
}

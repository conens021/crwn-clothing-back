using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.ColorDTOs;
using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.BLL.Services.ColorService;
using Microsoft.AspNetCore.Mvc;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            List<ColorDTO> colorDTOs = _colorService.GetAll();

            return Ok(colorDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetByiD(int id)
        {
            return Ok(_colorService.GetSafeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateColorDTO createColorDTO)
        {
            ColorDTO created = await _colorService.Create(createColorDTO);

            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateColorDTO colorDTO)
        {
            ColorDTO updated = await _colorService.Update(id, colorDTO);


            return Ok(updated);
        }

        [HttpPatch("{id}/name")]
        public async Task<IActionResult> PatchName(int id, [FromBody] string name)
        {
            ColorDTO updated = await _colorService.UpdateName(id, name);

            return Ok(updated);
        }

        [HttpPatch("{id}/value")]
        public async Task<IActionResult> PatchValue(int id, [FromBody] string value)
        {
            ColorDTO updated = await _colorService.UpdateValue(id, value);


            return Ok(updated);
        }
    }
}

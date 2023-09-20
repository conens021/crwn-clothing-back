using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.CategoryDto;
using CrwnClothing.BLL.DTOs.FiltersDTO;
using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {


            return Ok(_categoryService.GetAll());
        }

        [HttpGet("products")]
        public IActionResult GetAllWithProducts([FromQuery]PaginationDTO paginationDTO,[FromQuery] SortingDTO sortingDTO)
        {


            return Ok(_categoryService.GetAllWithProducts(paginationDTO,sortingDTO));
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {


            return Ok(_categoryService.GetCategoryByName(name));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO categoryDTO)
        {
            CategoryDTO created = await _categoryService.Create(categoryDTO);


            return Ok(created);
        }

        [HttpPatch("{id}/cover-image")]
        public async Task<IActionResult> UpdateCoverImage(int id, [FromBody] string image)
        {
            CategoryDTO updated = await _categoryService.UpdateCategoryImage(id, image);


            return Ok(updated);
        }
    }
}

using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpGet("products")]
        public IActionResult GetAllWithProducts()
        {
            return Ok(_categoryService.GetAllWithProducts());
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(_categoryService.GetCategoryByName(name));
        }

        [HttpGet("{name}/products")]
        public IActionResult GetByNameWithProducts(string name)
        {
            return Ok(_categoryService.GetCategoryWithProducts(name));
        }

        [HttpPost("")]
        public IActionResult CreateCategory(CategoryDTO categoryDTO)
        {
            return Ok(_categoryService.CreateCategory(categoryDTO));
        }
    }
}

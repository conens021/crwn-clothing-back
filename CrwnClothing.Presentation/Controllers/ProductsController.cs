using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.ProductDto;
using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.BLL.Services;
using CrwnClothing.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;
using CrwnClothing.Presentation.Models;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("client/{id}")]
        public IActionResult GetClientProductById(int id)
        {
            ProductWithCategoryAndSizesDTO productWithCategoryAndSizesDTO =
                _productService.GetClientProduct(id);


            return Ok(productWithCategoryAndSizesDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            ProductDTO productDTO = _productService.GetSafeById(id);


            return Ok(productDTO);
        }

        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] PaginationDTO paginationDTO,
            [FromQuery] SortingDTO sortingDTO,
            [FromQuery] ProductFilterModel filterModel)
        {

            return Ok(_productService.GetAll(paginationDTO, sortingDTO, filterModel.ToDTO()));
        }

        [HttpGet("category/{categoryName}")]
        public IActionResult GetAllByCategory(
        string categoryName,
       [FromQuery] PaginationDTO paginationDTO,
       [FromQuery] SortingDTO sortingDTO,
       [FromQuery] ProductFilterModel filterModel)
        {

            return Ok(_productService.GetAllByCategory(
                categoryName, paginationDTO, sortingDTO, filterModel.ToDTO()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO productDTO)
        {
            ProductDTO createdProduct = await _productService.Create(productDTO);


            return Created($"products/{createdProduct.Id}", createdProduct);
        }
    }
}

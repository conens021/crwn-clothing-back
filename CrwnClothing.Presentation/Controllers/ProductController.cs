using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrwnClothing.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            ProductDTO productDTO = _productService.GetProduct(id);


            return Ok(productDTO);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDTO productDTO)
        {
            ProductDTO createdProduct = _productService.CreateProduct(productDTO);


            return Created($"products/{createdProduct.Id}", createdProduct);
        }
    }
}

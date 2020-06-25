using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffe.Services.Product;
using SolarCoffe.Web.Serialization;
using SolarCoffe.Web.ViewModels;

namespace SolarCoffe.Web.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _productService = productService;
            _logger = logger;
            
        }
        [HttpGet("/api/products")]
        public ActionResult GetProduct()
        {
            _logger.LogInformation("Getting all products");
            var products =_productService.GetAllProducts();
            var productViewModels = products
                .Select(products => ProductMapper.SerializeProductModel(products));
            return Ok(productViewModels);
        }

        [HttpGet("/api/products/{id}")]
        public ActionResult GetProductById(int id)
        {
            _logger.LogInformation("Getting product");
            var product = _productService.GetProductById(id);
            return Ok(product);
        }
        
    }
}
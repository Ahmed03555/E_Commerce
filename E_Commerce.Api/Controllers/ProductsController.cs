using E_Commerce.Api.Attributes;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiBaseController
    {
        private readonly IProductServices productServices;

        public ProductsController(IProductServices productServices)
        {
            this.productServices=productServices;
        }

        [HttpGet]
        [RedisCach()]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProductsAsync([FromQuery]ProductQueryParam queryParam, CancellationToken ct)
        {
            var result = await productServices.GetAllProductAsync(queryParam, ct);
            return ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id , CancellationToken cancellationToken)
        {
            var result = await productServices.GetProductById(id, cancellationToken);
            return ToActionResult(result);

            
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GeyAllProductTypeAsync(CancellationToken cancellationToken)
        {
            var result = await productServices.GetAllTypeAsync(cancellationToken);
            return ToActionResult(result);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllProductBrandAsync(CancellationToken cancellationToken)
        {
            var result = await productServices.GetAllBrandAsync(cancellationToken);
            return ToActionResult(result);
        }
    }
}

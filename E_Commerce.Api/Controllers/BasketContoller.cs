using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Domain.Contarct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    public class BasketContoller : ApiBaseController
    {
        private readonly IBasketServices basketRepository;

        public BasketContoller(IBasketServices _basketRepository)
        {
            basketRepository= _basketRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BasketDto?>> GetBasket(string id, CancellationToken cancellationToken)
        {
            var result = await basketRepository.GetBasketAsync(id, cancellationToken);
            return ToActionResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto?>> CreateOrUpdateBasket(BasketDto basket, TimeSpan? timeSpan = null, CancellationToken cancellationToken = default)
        {
            var result = await basketRepository.CreateOrUpdateBasket(basket, timeSpan, cancellationToken);
            return ToActionResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = await basketRepository.DeleteBasketAsync(id,cancellationToken);
            return ToActionResult(result);
        }

    }
}

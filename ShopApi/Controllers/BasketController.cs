using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebShop.Business;
using WebShop.Business.Exceptions;
using WebShop.Business.Models;

namespace ShopApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBasketModel request)
        {
            try
            {
                Guid result = await basketRepository.CreateBasket(request);
                return Ok(result);
            }
            catch(UserNotFoundException ex)
            {
                return BadRequest($"User having name {ex.Name} not found.");
            }
            catch (DuplicateOpenBasketException)
            {
                return BadRequest("User can't create a new basket.");
            }
        }
        
        [HttpPut("{basketId}/article-line")]
        public async Task<IActionResult> AddItemToBasket([FromRoute] Guid basketId, [FromBody] AddItemModel model)
        {
            try
            {
                bool result = await basketRepository.AddToBasket(basketId, model);
                return Ok();
            }
            catch (BasketNotFoundException)
            {
                return NotFound("Basket not found");
            }
            catch (BasketClosedException)
            {
                return Problem("Can't add items to a closed basket");
            }
        }

        [HttpGet("{basketId}")]
        public async Task<IActionResult> GetBasket(Guid basketId)
        {
            try
            {
                var basket = await basketRepository.GetBasket(basketId);
                return Ok(basket);
            }
            catch(BasketNotFoundException)
            {
                return NotFound("Basket not found");
            }
        }

        [HttpPatch("{basketId}")]
        public async Task<IActionResult> PayBasket(Guid basketId)
        {
            try
            {
                await basketRepository.PayBasket(basketId);
                return Ok();
            }
            catch (BasketNotFoundException)
            {
                return NotFound("Basket not found");
            }
            catch (BasketClosedException)
            {
                return Problem("Basket already closed");
            }
        }

        [Route("/error")]
        public IActionResult HandleError() => Problem(detail: "Internal error occured");
    }
}

using WebShop.Business.Models;

namespace WebShop.Business
{
    public interface IBasketRepository
    {
        Task<bool> AddToBasket(Guid id, AddItemModel model);
        Task<Guid> CreateBasket(CreateBasketModel request);
        Task<BasketModel> GetBasket(Guid basketId);
        Task PayBasket(Guid basketId);
    }
}
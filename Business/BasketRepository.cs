using Microsoft.EntityFrameworkCore;
using WebShop.Business.Exceptions;
using WebShop.Business.Models;
using WebShop.DataAccess;
using WebShop.DataAccess.Models;

namespace WebShop.Business
{
    public class BasketRepository : IBasketRepository
    {
        private readonly WebShopContext context;
        private readonly decimal vat;

        public BasketRepository(WebShopContext context, decimal vat = 0.1m)
        {
            this.context = context;
            this.vat = vat;
        }

        public async Task<Guid> CreateBasket(CreateBasketModel model)
        {
            var dbCustomer = await context.Customers.FirstOrDefaultAsync(p => p.Name == model.Name);
            if (dbCustomer == null)
            {
                throw new UserNotFoundException(name: model.Name);
            }
            var basket = new Basket()
            {
                Customer = dbCustomer,
                PaysVAT = model.PaysVAT,
            };
            if (context.Baskets.Any(p => p.Customer.Name == model.Name && p.Closed == false))
            {
                throw new DuplicateOpenBasketException();
            }
            await context.Baskets.AddAsync(basket);
            await context.SaveChangesAsync();
            return basket.ID;
        }

        public async Task<bool> AddToBasket(Guid basketId, AddItemModel model)
        {
            Basket basket = await context.Baskets.FirstOrDefaultAsync(p => p.ID == basketId);
            if (basket == null)
            {
                throw new BasketNotFoundException();
            }
            if (basket.Closed)
            {
                throw new BasketClosedException();
            }
            basket.Items.Add(new Product()
            {
                Name = model.Item,
                Price = model.Price
            });
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<BasketModel> GetBasket(Guid basketId)
        {
            var basket = await context.Baskets
                .Include(p => p.Items)
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.ID == basketId);
            if (basket == null)
            {
                throw new BasketNotFoundException();
            }
            decimal totalNet = basket.Items?.Sum(p => p.Price) ?? 0;
            return new BasketModel()
            {
                Customer = basket.Customer.Name,
                TotalNet = totalNet,
                TotalGross = basket.PaysVAT ? totalNet * vat : totalNet,
                PaysVAT = basket.PaysVAT,
                Items = basket.Items.Select(p=> new ItemModel()
                {
                    Name = p.Name,
                    Price = p.Price,
                }).ToList(),
            };
        }

        public async Task PayBasket(Guid basketId)
        {
            var basket = await context.Baskets.FirstOrDefaultAsync(p => p.ID == basketId);
            if (basket == null)
            {
                throw new BasketNotFoundException();
            }
            if (basket.Closed)
            {
                throw new BasketClosedException();
            }
            basket.Closed = true;
            basket.Payed = true;
            await context.SaveChangesAsync();
        }
    }
}
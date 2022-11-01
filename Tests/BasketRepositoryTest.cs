using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WebShop.Business;
using WebShop.Business.Exceptions;
using WebShop.DataAccess;
using Xunit;

namespace Tests
{
    public class BasketRepositoryTest
    {
        readonly IBasketRepository repo;
        readonly WebShopContext dbContext;        
        const decimal vat = 0.1m;

        public BasketRepositoryTest()
        {
            var builder = new DbContextOptionsBuilder<WebShopContext>();
            builder.UseInMemoryDatabase(databaseName: "xxx");
            var dbContextOptions = builder.Options;
            dbContext = new WebShopContext(dbContextOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            repo = new BasketRepository(dbContext);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CreateBasketForExistingUser(bool paysVat)
        {
            await CreateVasile();
            Guid basketId = await repo.CreateBasket(new CreateBasketModel()
            {
                Name = "Vasile",
                PaysVAT = paysVat,
            });
            BasketModel basket = await repo.GetBasket(basketId);
            Assert.NotNull(basket);
            Assert.Empty(basket.Items);
            Assert.Equal(paysVat, basket.PaysVAT);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task AddToBasket(bool paysVat)
        {
            await CreateVasile();
            var items = new[]
            {
                new AddItemModel()
                {
                    Item = "tomato",
                    Price = 10,
                },
                new AddItemModel()
                {
                    Item = "cucumber",
                    Price = 5,
                }
            };
            Guid basketId = await repo.CreateBasket(new CreateBasketModel()
            {
                Name = "Vasile",
                PaysVAT = paysVat,
            });
            foreach(var item in items)
            {
                await repo.AddToBasket(basketId, item);
            }
            var basket = await repo.GetBasket(basketId);
            Assert.NotNull(basket);
            Assert.Equal(items.Length, basket.Items.Count);
            foreach(AddItemModel item in items)
            {
                Assert.Contains(basket.Items, p => p.Name == item.Item);
            }
            Assert.Equal(items.Sum(p => p.Price), basket.TotalNet);
            if (paysVat)
            {
                var withVat = items.Sum(p => p.Price) * vat;
                Assert.Equal(withVat, basket.TotalGross);
            }
            else
            {
                var noVat = items.Sum(p => p.Price);
                Assert.Equal(noVat, basket.TotalGross);
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CloseBasket(bool paysVat)
        {
            await CreateVasile();
            var items = new[]
            {
                new AddItemModel()
                {
                    Item = "tomato",
                    Price = 10,
                },
                new AddItemModel()
                {
                    Item = "cucumber",
                    Price = 5,
                }
            };
            Guid basketId = await repo.CreateBasket(new CreateBasketModel()
            {
                Name = "Vasile",
                PaysVAT = paysVat,
            });
            foreach (var item in items)
            {
                await repo.AddToBasket(basketId, item);
            }
            var basket = await repo.GetBasket(basketId);
            Assert.NotNull(basket);
            await repo.PayBasket(basketId);
            _ = Assert.ThrowsAnyAsync<BasketClosedException>(async () => await repo.PayBasket(basketId));
        }

        private async Task CreateVasile()
        {
            var name = "Vasile";
            dbContext.Customers.Add(new WebShop.DataAccess.Models.Customer()
            {
                Name = name,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
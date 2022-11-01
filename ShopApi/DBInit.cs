using WebShop.DataAccess;

namespace WebShop.Api
{
    public static class DBInit
    {
        public static void Init(WebShopContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;
            }

            context.Customers.Add(new DataAccess.Models.Customer()
            {
                Name = "Vasile",
            });
            context.SaveChanges();
        }
    }
}

namespace WebShop.Business.Models
{
    public class ItemModel
    {
        public ItemModel()
        {
        }

        public string Name { get; internal set; }
        public decimal Price { get; internal set; }
    }
}
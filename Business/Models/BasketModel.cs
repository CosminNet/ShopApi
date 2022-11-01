namespace WebShop.Business.Models
{
    public class BasketModel
    {
        public string Customer { get; internal set; }
        public object TotalGross { get; internal set; }
        public decimal TotalNet { get; internal set; }
        public bool PaysVAT { get; internal set; }
        public List<ItemModel> Items { get; set; }
    }
}
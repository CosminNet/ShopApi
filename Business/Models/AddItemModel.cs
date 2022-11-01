using System.ComponentModel.DataAnnotations;

namespace WebShop.Business.Models
{
    public class AddItemModel
    {
        public string Item { get; set; }
        public decimal Price { get; set; }
    }
}

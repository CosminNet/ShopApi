using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.DataAccess.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
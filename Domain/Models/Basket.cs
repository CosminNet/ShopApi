using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.DataAccess.Models
{
    public class Basket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public Customer Customer { get; set; }
        public bool PaysVAT {get;set;}       
        public bool Closed { get; set; }
        public bool Payed { get; set; }
        public ICollection<Product> Items { get; set; } = new List<Product>();
    }
}
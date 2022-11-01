using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.DataAccess.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebShop.Business.Models
{
    public class CreateBasketModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public bool PaysVAT { get; set; }
    }
}

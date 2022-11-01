using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Business.Models;

namespace WebShop.Business.Validators
{
    public class AddItemModelValidator : AbstractValidator<AddItemModel>
    {
        public AddItemModelValidator()
        {
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Item).NotNull().NotEmpty();
        }
    }
}

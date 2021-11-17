using Entities.Concrete;
using FluentValidation;
using System;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Product Name Boş geçilemez");
            RuleFor(p => p.ProductName).MinimumLength(2).WithMessage("Product Name Min 3 karakter olmalıdır");
            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage("Unit Price Boş Geçilemez");
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Product Name A ile başlanmalıdır"); ;


        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}

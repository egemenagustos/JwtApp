using FluentValidation;
using JwtApp.Front.Models;

namespace JwtApp.Front.ValidationRules
{
    public class ProductCreateValidator : AbstractValidator<CreateProductModel>
    {
        public ProductCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı boş geçilmemelidir.");
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("Ürün adı 15 karakterden fazla olmamalıdır.");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ürün adı 3 karakterden az olmamalıdır.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün fiyatı boş geçilmemelidir");

            RuleFor(x => x.Stock).NotEmpty().WithMessage("Ürün stoğu boş geçilmemelidir");
        }
    }
}

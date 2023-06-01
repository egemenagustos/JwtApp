using FluentValidation;
using Onion.JwpApp.Application.Features.CQRS.Commands;

namespace Onion.JwpApp.Application.ValidationRules.Products
{
    public class ProductUpdateValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id alanı boş geçilmemelidir.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı boş geçilmemelidir");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Ürün adı 20 karakterden fazla olmamalıdır.");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Ürün adı 5 karakterden az olmamalıdır.");

            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stok bilgisi boş geçilmemelidir.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat bilgisi boş geçilmemelidir.");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori bilgisi boş geçilmemelidir.");
        }
    }
}

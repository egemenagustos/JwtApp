using FluentValidation;
using Onion.JwpApp.Application.Features.CQRS.Commands;

namespace Onion.JwpApp.Application.ValidationRules.Categories
{
    public class CategoryCreateValidator : AbstractValidator<CreateCategoryCommandRequest>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Kategori adı boş geçilmemelidir");
            RuleFor(x => x.Definition).MaximumLength(15).WithMessage("Kategori adı 15 karakterden fazla olmamalıdır.");
            RuleFor(x => x.Definition).MinimumLength(5).WithMessage("Kategori adı 5 karakterden az olmamalıdır.");
        }
    }
}

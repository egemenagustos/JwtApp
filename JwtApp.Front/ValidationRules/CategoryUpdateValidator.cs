using FluentValidation;
using JwtApp.Front.Models;

namespace JwtApp.Front.ValidationRules
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateModel>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Kategori adı boş geçilmemelidir.");
            RuleFor(x => x.Definition).MaximumLength(15).WithMessage("Kategori adı 15 karakterden fazla olmamalıdır.");
            RuleFor(x => x.Definition).MinimumLength(3).WithMessage("Kategori adı 3 karakterden az olmamalıdır.");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id alanı boş geçilmemelidir.");
        }
    }
}

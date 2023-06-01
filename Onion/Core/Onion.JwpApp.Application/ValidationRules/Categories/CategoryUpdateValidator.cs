using FluentValidation;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwpApp.Application.ValidationRules.Categories
{
    public class CategoryUpdateValidator : AbstractValidator<UpdateCategoryCommandRequest>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id alanı boş geçilmemelidir.");
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Kategori adı boş geçilmemelidir");
            RuleFor(x => x.Definition).MaximumLength(15).WithMessage("Kategori adı 15 karakterden fazla olmamalıdır.");
            RuleFor(x => x.Definition).MinimumLength(5).WithMessage("Kategori adı 5 karakterden az olmamalıdır.");
        }
    }
}

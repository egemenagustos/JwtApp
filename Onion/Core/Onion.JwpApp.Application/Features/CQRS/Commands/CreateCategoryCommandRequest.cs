using MediatR;
using Onion.JwpApp.Application.Dtos;

namespace Onion.JwpApp.Application.Features.CQRS.Commands
{
    public class CreateCategoryCommandRequest : IRequest<CreatedCategoryDto>
    {
        public string? Definition { get; set; }
    }
}

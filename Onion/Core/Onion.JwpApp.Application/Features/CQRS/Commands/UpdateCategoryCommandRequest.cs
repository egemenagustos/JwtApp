using MediatR;
using Onion.JwpApp.Application.Dtos;

namespace Onion.JwpApp.Application.Features.CQRS.Commands
{
    public class UpdateCategoryCommandRequest : IRequest
    {
        public int Id { get; set; }

        public string Definition { get; set; }
    }
}

using MediatR;
using Onion.JwpApp.Application.Dtos;

namespace Onion.JwpApp.Application.Features.CQRS.Commands
{
    public class CreateProductCommandRequest : IRequest<CreatedProductDto>
    {
        public string? Name { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}

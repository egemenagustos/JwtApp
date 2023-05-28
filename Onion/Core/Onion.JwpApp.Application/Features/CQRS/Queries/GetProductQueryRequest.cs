using MediatR;
using Onion.JwpApp.Application.Dtos;

namespace Onion.JwpApp.Application.Features.CQRS.Queries
{
    public class GetProductQueryRequest : IRequest<ProductListDto>
    {
        public GetProductQueryRequest(int id)
        {

            Id = id;

        }

        public int Id { get; set; }
    }
}

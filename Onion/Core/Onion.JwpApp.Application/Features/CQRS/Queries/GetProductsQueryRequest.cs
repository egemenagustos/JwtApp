using MediatR;
using Onion.JwpApp.Application.Dtos;

namespace Onion.JwpApp.Application.Features.CQRS.Queries
{
    public class GetProductsQueryRequest : IRequest<List<ProductListDto>>
    {
    }
}

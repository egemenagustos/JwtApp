using MediatR;
using Onion.JwpApp.Application.Dtos;

namespace Onion.JwpApp.Application.Features.CQRS.Queries
{
    public class GetCategoryQueryRequest : IRequest<CategoryListDto>
    {
        public GetCategoryQueryRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

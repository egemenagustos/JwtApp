using MediatR;
using Onion.JwpApp.Application.Dtos;

namespace Onion.JwpApp.Application.Features.CQRS.Queries
{
    public class CheckUserQueryRequest : IRequest<CheckUserRequestDto>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

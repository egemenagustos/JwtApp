using MediatR;
using Onion.JwpApp.Application.Dtos;

namespace Onion.JwpApp.Application.Features.CQRS.Commands
{
    public class RegisterUserCommanRequest : IRequest<CreatedUserDto>
    {
        public string UserName { get; set; } = null!;
        public string Passoword { get; set; } = null!;
    }
}

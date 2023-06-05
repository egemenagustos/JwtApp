using MediatR;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest>
    {
        private readonly IRepository<AppUser> _appUserRepository;

        public UpdateUserCommandHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _appUserRepository.GetByIdAsync(request.Id);
            if(user != null)
            {
                user.UserName = request.UserName;
                user.Password = request.Password;
                user.Id = request.Id;
                await _appUserRepository.UpdateAsync(user);
            }
            return Unit.Value;
        }
    }
}

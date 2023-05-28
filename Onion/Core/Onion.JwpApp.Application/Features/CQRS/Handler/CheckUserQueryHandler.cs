using MediatR;
using Onion.JwpApp.Application.Dtos;
using Onion.JwpApp.Application.Features.CQRS.Queries;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    public class CheckUserQueryHandler : IRequestHandler<CheckUserQueryRequest, CheckUserRequestDto>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppRole> _appRoleRepository;

        public CheckUserQueryHandler(IRepository<AppUser> appUserRepository, IRepository<AppRole> appRoleRepository)
        {
            _appUserRepository = appUserRepository;
            _appRoleRepository = appRoleRepository;
        }

        public async Task<CheckUserRequestDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            var dto = new CheckUserRequestDto();
            var user = await _appUserRepository.GetByFilterAsync(x => x.UserName == request.UserName && x.Password == request.Password);
            if (user == null)
            {
                dto.IsExist = false;
            }
            else
            {
                dto.IsExist = true;
                dto.UserName = user.UserName;
                dto.Role = (await _appRoleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId))?.Definition;
                dto.Id = user.Id;
            }
            return dto;
        }
    }
}

using AutoMapper;
using MediatR;
using Onion.JwpApp.Application.Dtos;
using Onion.JwpApp.Application.Enums;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommanRequest, CreatedUserDto>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IRepository<AppUser> appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<CreatedUserDto> Handle(RegisterUserCommanRequest request, CancellationToken cancellationToken)
        {
            var result = await _appUserRepository.CreateAsync(new AppUser
            {
                AppRoleId = (int)RoleType.Member,
                Password = request.Passoword,
                UserName = request.UserName
            });
            return _mapper.Map<CreatedUserDto>(result);
        }
    }
}

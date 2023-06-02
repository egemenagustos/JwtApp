using AutoMapper;
using MediatR;
using Onion.JwpApp.Application.Dtos;
using Onion.JwpApp.Application.Features.CQRS.Queries;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQueryRequest, List<UserListDto>>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IRepository<AppUser> appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<List<UserListDto>> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _appUserRepository.GetAllAsync();
            return _mapper.Map<List<UserListDto>>(users);
        }
    }
}

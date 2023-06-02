using AutoMapper;
using MediatR;
using Onion.JwpApp.Application.Dtos;
using Onion.JwpApp.Application.Features.CQRS.Queries;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{

    public class GetAppRoleQueryHandler : IRequestHandler<GetAppRoleQueryRequest, List<AppRoleListDto>>
    {
        private readonly IRepository<AppRole> _appRoleRepository;
        private readonly IMapper _mapper;

        public GetAppRoleQueryHandler(IRepository<AppRole> appRoleRepository, IMapper mapper)
        {
            _appRoleRepository = appRoleRepository;
            _mapper = mapper;
        }

        public async Task<List<AppRoleListDto>> Handle(GetAppRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _appRoleRepository.GetAllAsync();
            return _mapper.Map<List<AppRoleListDto>>(result);
        }
    }
}

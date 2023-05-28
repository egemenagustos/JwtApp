using AutoMapper;
using MediatR;
using Onion.JwpApp.Application.Dtos;
using Onion.JwpApp.Application.Features.CQRS.Queries;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, ProductListDto>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductListDto> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByFilterAsync(x => x.Id == request.Id);
            return _mapper.Map<ProductListDto>(product);
        }
    }
}

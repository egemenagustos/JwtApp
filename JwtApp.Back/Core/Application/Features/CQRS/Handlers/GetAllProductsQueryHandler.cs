using AutoMapper;
using JwtApp.Back.Core.Application.Dto;
using JwtApp.Back.Core.Application.Features.CQRS.Queries;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductQueryRequest, List<ProductListDto>>
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IRepository<Product> productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductListDto>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _productsRepository.GetAllAsync();
            return _mapper.Map<List<ProductListDto>>(data);
        }
    }
}

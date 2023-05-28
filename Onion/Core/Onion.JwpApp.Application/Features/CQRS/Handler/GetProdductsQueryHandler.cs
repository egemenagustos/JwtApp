using AutoMapper;
using MediatR;
using Onion.JwpApp.Application.Dtos;
using Onion.JwpApp.Application.Features.CQRS.Queries;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    public class GetProdductsQueryHandler : IRequestHandler<GetProductsQueryRequest, List<ProductListDto>>
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IMapper _mapper;

        public GetProdductsQueryHandler(IRepository<Product> productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductListDto>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productsRepository.GetAllAsync();
            return _mapper.Map<List<ProductListDto>>(products);
        }
    }
}

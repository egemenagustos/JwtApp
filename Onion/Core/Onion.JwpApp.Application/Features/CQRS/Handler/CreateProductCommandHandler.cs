using AutoMapper;
using MediatR;
using Onion.JwpApp.Application.Dtos;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreatedProductDto>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreatedProductDto> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.CreateAsync(new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId
            });
            return _mapper.Map<CreatedProductDto>(result);
        }
    }
}

using MediatR;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedEntity = await _repository.GetByIdAsync(request.Id);
            if (updatedEntity != null)
            {
                updatedEntity.Stock = request.Stock;
                updatedEntity.Price = request.Price;
                updatedEntity.Name = request.Name;
                updatedEntity.CategoryId = request.CategoryId;
                await _repository.UpdateAsync(updatedEntity);
            }
            return Unit.Value;
        }
    }
}

using MediatR;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;

        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category != null)
            {
                category.Definition = request.Definition;
                await _repository.UpdateAsync(category);
            }
            return Unit.Value;
        }
    }
}

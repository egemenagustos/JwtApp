using MediatR;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwpApp.Application.Features.CQRS.Handler
{
    internal class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommandRequest>
    {
        private readonly IRepository<Category> _categoryRepository;

        public RemoveCategoryCommandHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedEntity = await _categoryRepository.GetByIdAsync(request.Id);
            if (deletedEntity != null)
            {
                await _categoryRepository.RemoveAsync(deletedEntity);
            }
            return Unit.Value;
        }
    }
}

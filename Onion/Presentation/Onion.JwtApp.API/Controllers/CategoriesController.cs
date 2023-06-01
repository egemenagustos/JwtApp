using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Features.CQRS.Queries;

namespace Onion.JwtApp.API.Controllers
{
    [Authorize(Roles = "Member,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateCategoryCommandRequest> _createValidator;
        private readonly IValidator<UpdateCategoryCommandRequest> _updateValidator;

        public CategoriesController(IMediator mediator, IValidator<CreateCategoryCommandRequest> createValidator, IValidator<UpdateCategoryCommandRequest> updateValidator)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetCategoriesQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCategoryQueryRequest(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommandRequest request)
        {
            var status = _createValidator.Validate(request);
            if (status.IsValid)
            {
                var result = await _mediator.Send(request);
                return Created("", result);
            }
            return BadRequest(status.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryCommandRequest request)
        {
            var status = _updateValidator.Validate(request);
            if (status.IsValid)
            {
                await _mediator.Send(request);
                return NoContent();
            }
            return BadRequest(status.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            await _mediator.Send(new RemoveCategoryCommandRequest(id));
            return Ok();
        }
    }
}

using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Features.CQRS.Queries;

namespace Onion.JwtApp.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateProductCommandRequest> _createValidator;
        private readonly IValidator<UpdateProductCommandRequest> _updateValidator;

        public ProductsController(IMediator mediator, IValidator<CreateProductCommandRequest> createValidator, IValidator<UpdateProductCommandRequest> updateValidator)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetProductsQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductQueryRequest(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommandRequest request)
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
        public async Task<IActionResult> Update(UpdateProductCommandRequest request)
        {
            var status = _updateValidator.Validate(request);
            if (status.IsValid)
            {
                await _mediator.Send(request);
                return NoContent();
            }
            return BadRequest(status.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _mediator.Send(new RemoveProductCommandRequest(id));
            return Ok();
        }
    }
}

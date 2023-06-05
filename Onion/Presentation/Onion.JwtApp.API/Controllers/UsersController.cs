using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Features.CQRS.Queries;

namespace Onion.JwtApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetUsersQueryRequest());
            if (result.Count > 0)
            {
                return Ok(result);
            }
            return BadRequest("Kullanıcılar bulunamadı.");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserCommandRequest request)
        {
          await _mediator.Send(request);
            return NoContent();
        }
    }
}

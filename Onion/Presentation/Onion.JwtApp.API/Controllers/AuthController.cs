using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Features.CQRS.Queries;
using Onion.JwtApp.API.Tools;

namespace Onion.JwtApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserCommanRequest request)
        {
            var result = await _mediator.Send(request);
            return Created("", result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(CheckUserQueryRequest request)
        {
            var dto = await _mediator.Send(request);
            if (dto.IsExist)
            {
                return Created("", JwtTokenGenerator.GenerateToken(dto));
            }
            else
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı!");
            }
        }
    }
}

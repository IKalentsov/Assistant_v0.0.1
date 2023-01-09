using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Assistant.Application.Auth.Query.Login;

namespace AssistantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        public AuthController(IMediator mediator)
            : base(mediator) { }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery query, CancellationToken cancellationToken)
        {
            var dto = await Mediator.Send(query, cancellationToken);

            return Ok(dto);
        }

        [HttpGet("check")]
        [Authorize]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssistantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ApiController
    {
        public PingController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public IActionResult Ping()
        {
            return Ok("Я живой");
        }
    }
}

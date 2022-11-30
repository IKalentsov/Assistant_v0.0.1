using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace AssistantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected ApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}

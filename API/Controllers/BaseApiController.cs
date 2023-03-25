using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        //access only in this function
        private IMediator _mediator;

        //access as long as they inherit from base api controller
        protected IMediator Mediator => 
            _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }

}
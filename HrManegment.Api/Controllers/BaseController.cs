using HR.LeaveManagement.Api.Middleware;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrManegment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IMediator _mediator;
        private readonly ILogger<BaseController> _logger;

        public BaseController() { }


        private BaseController(IMediator mediator,ILogger<BaseController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        
        

       
    }
}

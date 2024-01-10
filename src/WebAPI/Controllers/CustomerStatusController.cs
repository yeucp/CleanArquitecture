using Application.Customers.Status.Create;
using Application.Customers.Status.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/customer-status")]
    public class CustomerStatus : ApiController
    {
        private ISender _mediator;

        public CustomerStatus(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get() {
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            var userId = UserIdFromToken.GetUserId(token);

            var customerStatusResult = await _mediator.Send(new GetAllCustomerStatusQuery());

            return customerStatusResult.Match(
                statuses => Ok(statuses),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateCustomerStatusCommand command) 
        {

            var createCustomerStatusResult = await _mediator.Send(command);

            return createCustomerStatusResult.Match(
                customerStatus => Ok(),
                errors => Problem(errors)
            );
        }
    }
}

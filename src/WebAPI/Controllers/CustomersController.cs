using Application.Customers.Create;
using Application.Customers.Delete;
using Application.Customers.Exists;
using Application.Customers.GetAll;
using Application.Customers.GetById;
using Application.Customers.Update;
using Application.CustomerVIew.GetAll;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("customers")]
    public class Customers : ApiController
    {
        private readonly ISender _mediator;

        public Customers(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customersResult = await _mediator.Send(new GetAllCustomersQuery());
            return customersResult.Match(
                customers => Ok(customers),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customerResult = await _mediator.Send(new GetCustomerByIdQuery(id));
            return customerResult.Match(
                customer => Ok(customer),
                errors => Problem(errors)
            );
        }

        [HttpGet("/view")]
        public async Task<IActionResult> GetView()
        {
            var customersResult = await _mediator.Send(new GetCustomerViewQuery());
            return customersResult.Match(
                customers => Ok(customers),
                errors => Problem(errors)
            );
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> Exists(Guid id)
        {
            var customerResult = await _mediator.Send(new ExistsCustomerQuery(id));

            return customerResult.Match(
                customer => Ok(customer),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            var createCustomerResult = await _mediator.Send(command);

            return createCustomerResult.Match(
                customer => Created(),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            if(id != command.Id)
            {
                List<Error> errors = new() { 
                    Error.Validation("Customer.Id", "THe request Id does nt match with the url Id")
                };
                return Problem(errors);
            }

            var updateCustomerResult = await _mediator.Send(command);

            return updateCustomerResult.Match(
                customers => Ok(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteCustomerResult = await _mediator.Send(new DeleteCustomerCommand(id));

            return deleteCustomerResult.Match(
                customer => Ok(),
                error => Problem(error)
            );
        }
    }
}

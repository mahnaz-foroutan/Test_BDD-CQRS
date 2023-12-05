using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Application.Contracts;
using Application.Features.Customer.Queries.Dtos;
using Application.Features.Customer.Commands.Update;
using Application.Features.Customer.Commands.Delete;
using Application.Features.Customer.Queries.GetCustomerId;
using Application.Features.Customer.Queries.GetCustomersList;
using Application.Features.Customer.Commands.Insert;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        #region constructor

        private readonly IMediator _mediator;
       // private readonly ICustomerRepository _customerRepository;

        public CustomerController(IMediator mediator, ICustomerRepository customerRepository)
        {
            _mediator = mediator;
           // _customerRepository = customerRepository;
        }

        #endregion

        #region get all Customers
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<CustomerToReturnDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CustomerToReturnDto>>> GetCustomersAll()
        {
            var query = new GetCustomersListQuery();
            var Customers = await _mediator.Send(query);
            return Ok(Customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerToReturnDto>> GetCustomerById(int id)
        {
            var query = new GetCustomerIdQuery(id);
            var Customers = await _mediator.Send(query);
            return Ok(Customers);
        }

        #endregion

        #region Insert Customer
        [HttpPost(Name = "InsertCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertCustomer([FromBody] InsertCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region update Customer
        [HttpPut(Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateCustomer([FromBody] UpdateCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region delete Customer
        [HttpDelete("{id}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _mediator.Send(new DeleteCommand { Id = id });
            return NoContent();
        }

        #endregion

       
      
    }
}

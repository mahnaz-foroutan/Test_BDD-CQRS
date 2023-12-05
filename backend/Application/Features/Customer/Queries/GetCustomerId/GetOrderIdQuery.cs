using System.Collections.Generic;
using MediatR;
using Application.Features.Customer.Queries.Dtos;

namespace Application.Features.Customer.Queries.GetCustomerId
{
    public class GetCustomerIdQuery : IRequest<CustomerToReturnDto>
    {
        public int Id { get; set; }

        public GetCustomerIdQuery(int id)
        {
            Id = id;
        }
    }
}

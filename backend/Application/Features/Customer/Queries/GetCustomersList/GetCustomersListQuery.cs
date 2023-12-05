using Application.Features.Customer.Queries.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customer.Queries.GetCustomersList
{
    public class GetCustomersListQuery : IRequest<IReadOnlyList<CustomerToReturnDto>>
    {
    }
}

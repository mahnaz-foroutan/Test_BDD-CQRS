using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Application.Contracts;
using Application.Features.Customer.Queries.Dtos;
using Domain.Entities;

namespace Application.Features.Customer.Queries.GetCustomerId
{
    public class GetCustomerIdQueryHandler : IRequestHandler<GetCustomerIdQuery, CustomerToReturnDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerToReturnDto> Handle(GetCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var CustomersList = await _customerRepository.GetByIdAsync(request.Id);

            return _mapper.Map<CustomerToReturnDto>(CustomersList);
        }

    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Application.Contracts;
using Application.Features.Customer.Queries.Dtos;

namespace Application.Features.Customer.Queries.GetCustomersList
{
    public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery,IReadOnlyList<CustomerToReturnDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersListQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CustomerToReturnDto>> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var customersList  = await _customerRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<Domain.Entities.Customer>, IReadOnlyList<CustomerToReturnDto>>(customersList);
        }

        
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts;
using Domain.Entities;

namespace Application.Features.Customer.Commands.Insert
{
    public class InsertCommandHandler : IRequestHandler<InsertCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<InsertCommandHandler> _logger;

        public InsertCommandHandler(ICustomerRepository customerRepository, IMapper mapper, ILogger<InsertCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(InsertCommand request, CancellationToken cancellationToken)
        {
            var customerEntity = _mapper.Map<Domain.Entities.Customer>(request);
            var newCustomer = await _customerRepository.AddAsync(customerEntity);

            _logger.LogInformation($"Customer {newCustomer.Id} is successfully created");

            return Unit.Value;
        }

    }
}
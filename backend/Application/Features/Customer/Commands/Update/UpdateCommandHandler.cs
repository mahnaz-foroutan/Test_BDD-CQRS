using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts;
using Application.Exceptions;
using Domain.Entities;

namespace Application.Features.Customer.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCommandHandler> _logger;

        public UpdateCommandHandler(ICustomerRepository customerRepository, IMapper mapper, ILogger<UpdateCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var customerForUpdate = await _customerRepository.GetByIdAsync(request.Id);
            if (customerForUpdate == null)
            {
                _logger.LogError("Customer is not exists");
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            _mapper.Map(request, customerForUpdate, typeof(UpdateCommand), typeof(Domain.Entities.Customer));

            await _customerRepository.UpdateAsync(customerForUpdate);
            _logger.LogInformation($"Customer {customerForUpdate.Id} is successfully updated");

            return Unit.Value;
        }
    }
}

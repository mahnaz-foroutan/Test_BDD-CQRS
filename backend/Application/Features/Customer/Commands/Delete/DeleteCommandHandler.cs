using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts;
using Domain.Entities;
using Application.Exceptions;

namespace Application.Features.Customer.Commands.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<DeleteCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteCommandHandler(ICustomerRepository customerRepository, ILogger<DeleteCommandHandler> logger, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var customerForDelete = await _customerRepository.GetByIdAsync(request.Id);

            if (customerForDelete == null)
            {
                _logger.LogError("Customer not exists");
                throw new NotFoundException(nameof(Customer), request.Id);
            }
            else
            {
                await _customerRepository.DeleteAsync(customerForDelete);
                _logger.LogInformation($"Customer {customerForDelete.Id} is successfully deleted");
            }

            return Unit.Value;
        }
    }
}
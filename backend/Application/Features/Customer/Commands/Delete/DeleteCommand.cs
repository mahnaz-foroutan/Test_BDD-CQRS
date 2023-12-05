using MediatR;

namespace Application.Features.Customer.Commands.Delete
{
    public class DeleteCommand : IRequest
    {
        public int Id { get; set; }
    }
}

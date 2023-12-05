using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Contracts
{
   public interface ICustomerRepository : IAsyncRepository<Customer>
    {
    
    }
}

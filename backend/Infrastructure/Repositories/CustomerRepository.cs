using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Features.Customer.Queries.Dtos;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class CustomerRepository: RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext) : base(dbContext) { }


    }
}

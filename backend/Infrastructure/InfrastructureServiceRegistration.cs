using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Contracts;
using Infrastructure.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("CustomerConnectionString"));
            });
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
           
            return services;
        }
    }
}
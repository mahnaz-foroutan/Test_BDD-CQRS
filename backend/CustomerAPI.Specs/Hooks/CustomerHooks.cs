using Application.Contracts;
using BoDi;
using CustomerAPI.Specs.Repositories;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Specs.Hooks
{
    [Binding]
    public class CustomerHooks
    {
        private readonly IObjectContainer _objectContainer;
        private const string AppSettingsFile = "appsettings.json";
       // private readonly WebApplicationFactory<Program> _factory;
      //  private HttpClient _client;
        private  IServiceScopeFactory _scopeFactory;
        private IServiceScope _scope;
        private ICustomerRepository _repository;

        public CustomerHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public async Task RegisterServices()
        {
            var factory = GetWebApplicationFactory();
            _scopeFactory = factory.Services.GetService<IServiceScopeFactory>();
            _scope = _scopeFactory.CreateScope();
            _repository = _scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
            await ClearData(factory);
            _objectContainer.RegisterInstanceAs(factory);
            var jsonFilesRepo = new JsonFilesRepository();
            _objectContainer.RegisterInstanceAs(jsonFilesRepo);
            //var repository = (ICustomerRepository)factory.Services.GetService(typeof(ICustomerRepository))!;
            //_objectContainer.RegisterInstanceAs(repository);
        }

        private WebApplicationFactory<Program> GetWebApplicationFactory() =>
            new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    IConfigurationSection? configSection = null;
                    builder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), AppSettingsFile));
                      //  configSection = context.Configuration.GetSection(nameof(ConnectionStrings));
                    });
                    builder.ConfigureTestServices(services =>
                     services.AddDbContext<CustomerContext>(options =>
                     {
                         // Using an in-memory SQLite database. Every test run will have its own isolated database
                         options.UseSqlite("ConnectionStrings=:CustomerConnectionString:");
                     }));
                });

        private async Task ClearData(
            WebApplicationFactory<Program> factory)
        {
            if (_repository is not ICustomerRepository repo ) return;
            var entities = await _repository.GetAllAsync();
            foreach (var entity in entities)
                await _repository.DeleteAsync(entity);
        }
    }
    }

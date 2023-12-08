using Application.Contracts;
using CustomerAPI.Specs.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http.Json;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;
using Xunit;
using CustomerAPI.Specs.Support;
using Microsoft.Extensions.DependencyInjection;
using BoDi;
using Google.Protobuf.WellKnownTypes;
using FluentAssertions.Common;
using Microsoft.Extensions.Options;

namespace CustomerAPI.Specs.StepDefinitions
{
    [Binding]
    public class CustomerApiStepDefinitions: IDisposable
    {
        private const string BaseAddress = "http://localhost:5208/api/v1/";
        private readonly WebApplicationFactory<Program> _factory;
        private  HttpClient _client;
        private readonly IServiceScopeFactory _scopeFactory;
        private IServiceScope _scope;
        private ICustomerRepository _repository;
        private HttpResponseMessage _response;
        
        public JsonFilesRepository JsonFilesRepo { get; }
        private Customer? Entity { get; set; }

        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public CustomerApiStepDefinitions(WebApplicationFactory<Program> factory, JsonFilesRepository jsonFilesRepo)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            _scope = _scopeFactory.CreateScope();
    
            _repository = _scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
            JsonFilesRepo = jsonFilesRepo;
        }

      

        [AfterScenario]
        public void CleanupScenario()
        {
            Dispose();
        }

      
        public void Dispose()
        {
            _scope?.Dispose();
            _scope = null;
        }

        [Given(@"I am a client")]
        public void GivenIAmAClient()
        {
            _client = _factory.CreateDefaultClient(new Uri(BaseAddress));
        }

        [Given(@"the repository has customer data")]
        public async Task GivenTheRepositoryHasCustomerData()
        {
            var customerJson = JsonFilesRepo.Files["customers.json"];
           
                var customers = JsonSerializer.Deserialize<IList<Customer>>(customerJson, _jsonSerializerOptions);
             
                if (customers != null)
                    foreach (var customer in customers)
                        await _repository.AddAsync(customer);
           
        }

        [When(@"I make a GET request to '([^']*)'")]
        public async Task WhenIMakeAGETRequestTo(string customer)
        {
            _response = await _client.GetAsync(customer);
        }

        [When(@"I make a GET request with id '([^']*)' to '([^']*)'")]
        public async Task WhenIMakeAGETRequestWithIdTo(int id, string customer)
        {
            _response = await _client.GetAsync($"{customer}/{id}");
        }

        [When(@"I make a POST request with '([^']*)' to '([^']*)'")]
        public async Task WhenIMakeAPOSTRequestWithTo(string file, string customer)
        {
            var json = JsonFilesRepo.Files[file];
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            _response = await _client.PostAsync(customer, content);
        }

        [When(@"I make a PUT request with '([^']*)' to '([^']*)'")]
        public async Task WhenIMakeAPUTRequestWithTo(string file, string customer)
        {
            var json = JsonFilesRepo.Files[file];
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            _response = await _client.PutAsync(customer, content);
        }

        [When(@"I make a DELETE request with id '([^']*)' to '([^']*)'")]
        public async Task WhenIMakeADELETERequestWithIdTo(int id, string customer)
        {
            _response = await _client.DeleteAsync($"{customer}/{id}");
        }

        [Then(@"the response status code is '(.*)'")]
        public void ThenTheResponseStatusCodeIs(int statusCode)
        {
            var expected = (HttpStatusCode)statusCode;
            Assert.Equal(expected, _response.StatusCode);
        }


        [Then(@"the response json should be '(.*)'")]
        public async Task ThenTheResponseDataShouldBe(string file)
        {
            var expected = JsonFilesRepo.Files[file];
            var response = await _response.Content.ReadAsStringAsync();
            var actual = response.JsonPrettify();
            Assert.Equal(expected, actual);
        }

    }
}

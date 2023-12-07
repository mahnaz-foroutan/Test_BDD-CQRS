using System;
using TechTalk.SpecFlow;

namespace TestProject
{
    [Binding]
    public class FeatureCustomerStepDefinitions
    {
        public BddTestsApplicationFactory _factory = new BddTestsApplicationFactory();
        public HttpClient _client { get; set; } = null!;
        private HttpResponseMessage _response { get; set; } = null!;

        public FeatureCustomerStepDefinitions()
        {
            _client = _factory.CreateDefaultClient(new Uri($"http://localhost/"));
        }

        [Given(@"the system has customers")]
        public void GivenTheSystemHasCustomers()
        {
            throw new PendingStepException();
        }

        [When(@"I request all customers")]
        public void WhenIRequestAllCustomers()
        {
            throw new PendingStepException();
        }

        [Then(@"the response should include a list of customers")]
        public void ThenTheResponseShouldIncludeAListOfCustomers()
        {
            throw new PendingStepException();
        }

        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int p0)
        {
            throw new PendingStepException();
        }

        [When(@"I request a customer by ID")]
        public void WhenIRequestACustomerByID()
        {
            throw new PendingStepException();
        }

        [Then(@"the response should include the customer's details")]
        public void ThenTheResponseShouldIncludeTheCustomersDetails()
        {
            throw new PendingStepException();
        }

        [Given(@"I have customer data to insert")]
        public void GivenIHaveCustomerDataToInsert()
        {
            throw new PendingStepException();
        }

        [When(@"I submit the new customer data")]
        public void WhenISubmitTheNewCustomerData()
        {
            throw new PendingStepException();
        }

        [Then(@"the system should create a new customer")]
        public void ThenTheSystemShouldCreateANewCustomer()
        {
            throw new PendingStepException();
        }

        [Given(@"I have updated customer data and the customer exists")]
        public void GivenIHaveUpdatedCustomerDataAndTheCustomerExists()
        {
            throw new PendingStepException();
        }

        [When(@"I submit an update for the customer")]
        public void WhenISubmitAnUpdateForTheCustomer()
        {
            throw new PendingStepException();
        }

        [Then(@"the customer's data should be updated in the system")]
        public void ThenTheCustomersDataShouldBeUpdatedInTheSystem()
        {
            throw new PendingStepException();
        }

        [Given(@"I have the ID of a customer to delete")]
        public void GivenIHaveTheIDOfACustomerToDelete()
        {
            throw new PendingStepException();
        }

        [When(@"I submit a request to delete the customer")]
        public void WhenISubmitARequestToDeleteTheCustomer()
        {
            throw new PendingStepException();
        }

        [Then(@"the customer should be deleted from the system")]
        public void ThenTheCustomerShouldBeDeletedFromTheSystem()
        {
            throw new PendingStepException();
        }
    }
}

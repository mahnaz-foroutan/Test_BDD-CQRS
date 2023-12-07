Feature: FeatureCustomer

A short summary of the feature
@tag1
  Scenario: Getting the list of all customers
    Given the system has customers
    When I request all customers
    Then the response should include a list of customers
    And the response code should be 200

  Scenario: Getting a customer by ID
    Given the system has customers
    When I request a customer by ID
    Then the response should include the customer's details
    And the response code should be 200

  Scenario: Inserting a new customer
    Given I have customer data to insert
    When I submit the new customer data
    Then the system should create a new customer
    And the response code should be 204

  Scenario: Updating a customer
    Given I have updated customer data and the customer exists
    When I submit an update for the customer
    Then the customer's data should be updated in the system
    And the response code should be 204

  Scenario: Deleting a customer
    Given the system has customers
    And I have the ID of a customer to delete
    When I submit a request to delete the customer
    Then the customer should be deleted from the system
    And the response code should be 204

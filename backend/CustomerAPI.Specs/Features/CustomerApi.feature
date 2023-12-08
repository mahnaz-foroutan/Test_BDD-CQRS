Feature: CustomerApi

A short summary of the feature

@tag1

Scenario: Get All
	Given I am a client
	And the repository has customer data
	When I make a GET request to 'customer'
	Then the response status code is '200'
	And the response json should be 'customers.json'

Scenario: Get customer by id
	Given I am a client
	And the repository has customer data
	When I make a GET request with id '3' to 'customer'
	Then the response status code is '200'
	And the response json should be 'customer.json'

Scenario: Add customer
	Given I am a client
	When I make a POST request with 'customer.json' to 'customer'
	Then the response status code is '204'

Scenario: Update customer
	Given I am a client
	And the repository has customer data
	When I make a PUT request with 'customer.json' to 'customer'
	Then the response status code is '204'
	
	
Scenario: Remove customer
	Given I am a client
	And the repository has customer data
	When I make a DELETE request with id '3' to 'customer'
	Then the response status code is '204'

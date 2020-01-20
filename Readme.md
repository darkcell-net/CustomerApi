# Customer API

Based on the following requirements from a coding test.

## Problem to Solve
Have the candidate build a simple API that allows:
1. Adding customers.
First name, last name and date of birth fields.
2. Editing customers.
3. Deleting customers.
4. Searching for a customer by partial name match (first or last name).

## Tech

1. ASP.NET Core 2.2 API
2. In memory entity framework store
3. Dependency injection
4. Basic XUnit tests
5. Swagger / OpenAPI support

## Interface

When running the site locally you can access it via port 6000.

1. Swagger info
HTTP GET `localhost:6000/swagger/v1/swagger.json`.
2. Searching for customers
HTTP GET `localhost:6000/customers?FirstName=first&LastName=last`.
3. Adding a new customer
HTTP POST `localhost:6000/customers` using a `Customer` body with a `application/vnd.darkcell.customer.v1+json` content type.
Returns the created customer with its associated id.
4. Updating an existing customer
HTTP PUT `localhost:6000/customers` using a `Customer` body with a `application/vnd.darkcell.customer.v1+json` content type.
5. Deleting a customer
HTTP DELETE `localhost:6000/customers/id`.
`id` is generated when adding the customer.

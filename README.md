# School Service

This is a simple demo project.

## Installation Notes

- Install Microsoft SQL Server. I'm using the Developer Edition 2016, but it should work with anything 2014 or up.
- Install Visual Studio 2017. It's pretty version agnostic. I'm peeved that Testing Code Coverage is restricted to Enterprise edition.
- Run the `initializer.sql` script under `School/Database/`. Congrats, your DB is built!
- Optional: create a new user ID to access the `School` database.
- Get a connection string that works.
- Replace the connection strings in:

	- `School/School.Tests/Integration/Data/TestBase.cs`, variable name is `TEST_CONNECTION_STRING`.
	- 'School/School.API/appsettings.Development.json', variable name is `ConnectionStrings` > `SchoolDB`.

- Build the project.
- Run the tests. They should pass.
- Hit F5 to run the API.
- Swagger should show up.
- Go crazy.     
      

## Current Technologies:

- WebAPI with Swagger UI
- Entity Framework
- Unit Tests
- Integration Tests
- Microsoft SQL Server 2016
- .NET Core 2.2

## To Iterate:

### WebAPI

- Add authorization.
- Add global error handling.
- Add logging.

### Entity Framework

- Add environments - Dev, Cert, Prod.
- Add logging.

### Tests (Unit, Integration)

- Add environments.
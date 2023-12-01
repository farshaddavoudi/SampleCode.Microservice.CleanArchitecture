

# Technologies:
**.NET 7** / **CorrelationId Handling** / **CQRS with MediatR** / **Global Exception Handling**
 / **Serilog for diagnostic logs with many enrichers and Seq sink** / **EF Core 7 with SQLServer**
 / **Postman document for APIs** / **Audit.NET logs for EF Core and WebAPI Audit Logs written in MongoDB** 
 / **Clean Architecture** / **JWT Token Auth** / **Hangfire [without dashbaord]**

# Architecture
 It is Clean Architechture, the one more aligned withJson Tylor and Nick Chapsas.

### Domain:
 - Things related to the domain (a little like DDD). No logic comes here. 
 - This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application:
 - Orchestrate the application. This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers and DTOs. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure:
 - This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Installation:

Install VS Code and the following extensions: `C# (Microsoft)`, `C# Snippets`, `C# Extensions`.

Also, install the .NET SDK.

### Dependency Injection Registration:

Utilize a package like `AutoRegisterDi` in conjunction with the *DiInstaller* pattern.

### Project Architecture:

Implement *Clean Architecture* with the following projects:

- API (WebAPI project)
- Application (Class Library)
- Infrastructure (Class Library)
- Domain (Class Library)

> It is the Clean Architecture varient which is more aligned with Json Tylor and Nick Chapsas. 

## Clean Code:

Follow these rules:

- Use clear, meaningful, and complete names for classes, methods, parameters, and variables without unnecessary shortening. Long names are acceptable.
- Adhere to the Single Responsibility Principle: Create lightweight classes, especially with the help of CQRS.
- Keep comments short and concise; names should be self-explanatory. Avoid long comments.

## Mapping:

While *AutoMapper* is used, consider other libraries or manual mapping for better performance. AutoMapper adversely affects project performance, especially for high-load applications.

## Databases / ORMs:

- SQL Server [main db]
- EF Core Code First configured with Fluent API
- Generic Repository pattern with Unit of Work and Specification Pattern
- Dapper [configured with DI; provided a simple use case as an example]
- PostgreSQL [store Hangfire jobs]
- MongoDB [store API Request/Response and EF Core audit logs]
- Seq [Serilog logs sink]
  
## Coding Pattern: CQRS (implemented using MediatR library)

- Use the `.Send()` method to dispatch requests to appropriate handlers.
- Use the `.Publish()` method to leverage *MediatR*'s notification feature for *Eventing*
- Utilize *FluentValidation* for validating requests configured in *MediatR Pipeline Behaviours*.
- Make the database operations transactional in *MediatR*'s command handlers again using a *Pipeline Behaviour*
  
## Authentication:

- Utilize *JWT* tokens containing an encrypted userId.
- Provide complete user claims through middleware (between `app.UseAuthentication()` and `app.UseAuthorization()`) using *Redis* to enhance speed and avoid adverse effects on performance.
- Use *RefreshToken* to refresh/re-issue the token (stored in *Redis* for per-request checks to enable instant user disabling control).
  
## Background Jobs:

- Use *Hangfire* to run background jobs.
- Configured a secure dashboard with Administrator role restriction to view it.
- Use it for syncing the users' table.
- Use the *Hosted Sevices* for listening *RabbitMQ* messages

## Tracking Performance:

- *K6* test tool [Normal/Stress/Spike/Soak load/performance tests]
- *MiniProfiler* library [only in development]
- *Audit.NET* library [Request/Response + EF Core audit logs stored in MongoDB]
  
## SignalR:

- Create a notification Hub for sending messages to the clients (Make `NotificationsHub` strongly typed).
- Use *Redis* as a backplane for scale-out.
- Develop a protected *Minimal API* endpoint to send a message to all connected clients.
  
## gRPC:

- Utilize gRPC to request user permission for a controller's `action` from other microservices and obtain user roles and other information.
- Aim for high-performance connections, especially considering it is called per request.
- Employ gRPC, which internally uses a service based on Redis cache, making this process very fast.
  
## Message Broker: RabbitMQ

Publish messages, such as a user added or changed, so that other services can listen and be notified.

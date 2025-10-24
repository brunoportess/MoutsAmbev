# 🧩 Ambev Developer Evaluation – CQRS Backend

This project is a **.NET 8 Web API** following the principles of **CQRS (Command Query Responsibility Segregation)**.  
It provides endpoints for managing products, carts, and sales using **PostgreSQL** as the database.

---

## 🚀 Technologies Used

- **.NET 8.0**
- **Entity Framework Core**
- **PostgreSQL**
- **Bogus** (for fake data generation)
- **MediatR**
- **AutoMapper**
- **Swagger / OpenAPI**
- **Docker and Docker Compose**
- **Serilog** (logging)
- **JWT Authentication**

---

## ⚙️ Prerequisites

Before running the project, make sure you have installed:

- [Docker Desktop](https://www.docker.com/)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- (optional) [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- (optional) [Postman](https://www.postman.com/) for API testing

---

## 🐳 Running with Docker (recommended)

### 1️⃣ Clean build

```bash
docker compose down -v
docker compose build --no-cache
```

### 2️⃣ Start containers

```bash
docker compose up
```

This will start:
- **ambev.developerevaluation.webapi** → .NET API (port `8080`)
- **ambev.developerevaluation.database** → PostgreSQL (port `5432`)

The API will be available at:  
👉 [http://localhost:8080/swagger](http://localhost:8080/swagger)

---

### 3️⃣ Connection between containers

The API uses the following environment variable inside `docker-compose.yml`:

```yaml
ConnectionStrings__DefaultConnection=Host=ambev.developerevaluation.database;Port=5432;Database=developer_evaluation;Username=developer;Password=ev@luAt10n;Pooling=true;SSL Mode=Disable;Trust Server Certificate=true
```

This ensures that the API can connect properly to the PostgreSQL container.

---

### 4️⃣ Logs and troubleshooting

Check logs with:

```bash
docker logs -f ambev_developer_evaluation_webapi
```

To rebuild everything from scratch:

```bash
docker compose down -v
docker compose build --no-cache
docker compose up
```

---

## 💻 Running Locally (without Docker)

### 1️⃣ Create a PostgreSQL database

| Parameter | Value |
|------------|--------|
| Host | localhost |
| Port | 5432 |
| User | developer |
| Password | ev@luAt10n |

---

### 2️⃣ Update `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=developer_evaluation;Username=developer;Password=ev@luAt10n;Pooling=true;SSL Mode=Disable;Trust Server Certificate=true"
}
```

---

### 3️⃣ Create migrations and update the database

```bash
dotnet ef migrations add InitialCreate --startup-project ../Ambev.DeveloperEvaluation.WebApi
dotnet ef database update --startup-project ../Ambev.DeveloperEvaluation.WebApi
```

---

### 4️⃣ Run the application

```bash
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

Access:  
👉 [http://localhost:5119/swagger](http://localhost:5119/swagger)

---

## 🧪 Sample Data

The `DatabaseSeeder` class automatically populates the database with:

- **30 products**
- **5 carts**
- **5 sales**

To disable automatic seeding, comment the following block in `Program.cs`:

```csharp
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        DatabaseSeeder.SeedAsync(app.Services, logger).GetAwaiter().GetResult();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to execute seeder. Continuing without seed data.");
    }
}
```

---

## 🔐 Authentication

The API uses **JWT (JSON Web Token)** for authentication.  
Include the token in the Authorization header for protected endpoints:

```
Authorization: Bearer {your_token_here}
```

---

## 🧱 Project Structure

```
src/
├── Ambev.DeveloperEvaluation.WebApi/      → Presentation layer (Controllers, Middleware, Swagger)
├── Ambev.DeveloperEvaluation.Application/ → Use cases (Handlers, DTOs, Commands, Queries)
├── Ambev.DeveloperEvaluation.Domain/      → Domain entities and business logic
├── Ambev.DeveloperEvaluation.ORM/         → EF Core context, mappings, and migrations
├── Ambev.DeveloperEvaluation.Common/      → Logging, Security, and Helpers
```

---

## 🧩 CQRS Architecture Overview

This project implements **CQRS (Command Query Responsibility Segregation)** to separate read and write operations, improving scalability, performance, and maintainability.

### 📤 Commands
Responsible for **changing application state** (e.g., creating a sale, updating a cart, registering a product).

```csharp
public record CreateSaleCommand(Guid CustomerId, string CustomerName) : IRequest<SaleResponse>;
```

### 📥 Queries
Responsible for **reading data**, without modifying state.

```csharp
public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse>;
```

### ⚙️ MediatR
**MediatR** acts as a mediator, dispatching Commands and Queries to their respective Handlers, ensuring a clean and decoupled architecture.

---

## 🧩 Simplified CQRS Flow Diagram

```
┌──────────────┐
│   Controller │
└──────┬───────┘
       │
       ▼
┌──────────────┐
│   MediatR    │
└──────┬───────┘
       │
 ┌─────┴────────┐
 │ Command/Query│
 └─────┬────────┘
       ▼
┌──────────────┐
│   Handler    │
└──────┬───────┘
       ▼
┌──────────────┐
│  Repository  │
└──────┬───────┘
       ▼
┌──────────────┐
│   Database   │
└──────────────┘
```

---

## 🧰 Useful Commands

| Description | Command |
|--------------|----------|
| Build solution | `dotnet build` |
| Run tests | `dotnet test` |
| Run locally | `dotnet run --project src/Ambev.DeveloperEvaluation.WebApi` |
| Run with Docker | `docker compose up --build` |
| Reset containers | `docker compose down -v && docker compose up --build` |

---

## 🧑‍💻 Logging and Monitoring

The project uses **Serilog** for structured logging, configured for console and file outputs.  
Logs include:
- Application startup
- Database connection
- Health Checks
- Caught exceptions

---

## 📜 License

This project was developed for **technical evaluation purposes**.

---

## 💡 Tips for Technical Reviewers

This section highlights key technical and architectural decisions that demonstrate code quality, scalability, and adherence to modern backend standards.

### 🧱 Layered Architecture
The solution follows a **clean layered structure**, ensuring clear separation of concerns:
- **Domain Layer** — pure business logic and entities
- **Application Layer** — CQRS handlers, commands, queries, and DTOs
- **Infrastructure (ORM)** — persistence logic using Entity Framework Core
- **Presentation Layer (WebApi)** — controllers, middleware, and API exposure

This separation facilitates maintainability, testability, and scalability in complex enterprise environments.

### ⚙️ Dependency Injection and Inversion of Control
All dependencies are registered through the built-in **.NET Dependency Injection container**, ensuring:
- Low coupling between layers
- High flexibility for mocking dependencies during tests
- Better control of lifecycle management

### 🧩 CQRS + MediatR Integration
The **CQRS pattern** was applied with **MediatR** as a mediator between the presentation and application layers, ensuring:
- Commands handle **write operations**
- Queries handle **read operations**
- Controllers remain thin and focused only on request/response orchestration

### 🔐 Authentication & Security
JWT authentication was implemented to secure endpoints and demonstrate real-world authorization handling.  
It can be easily extended to include **role-based policies** or **external provider tokens**.

### 🧰 Database and ORM
The project uses **Entity Framework Core** with **Fluent API configurations**, following best practices:
- Explicit table mapping via `EntityTypeConfiguration`
- Type-safe configuration for owned entities (`OwnsMany`)
- Clear separation of **Migrations** and **Context**

### 🧪 Testing and Seed Strategy
The **data seeding mechanism** (powered by **Bogus**) provides reproducible datasets for testing and demonstration, allowing quick validation of endpoints.

### 🧠 Extensibility
The architecture is designed for future evolution:
- New Commands and Queries can be added without modifying existing logic
- Repositories can be replaced with other data sources (e.g., NoSQL, APIs)
- Handlers can integrate **validation**, **caching**, or **logging** with minimal refactoring

### ✅ Summary
This project showcases:
- Clean, production-ready architecture
- Adherence to SOLID and CQRS principles
- Enterprise-grade design with maintainability and scalability in mind

---

✅ **Author:** Bruno de Assis Portes  
💼 **LinkedIn:** [linkedin.com/in/bruno-portes](https://linkedin.com/in/bruno-portes)

# E-Commerce RESTful API

A scalable and maintainable E-Commerce Backend API built with **.NET Core** following **Clean Architecture** principles, **CQRS Pattern**, and industry best practices.

---

## 🏗 Project Architecture

The solution is structured using Clean Architecture to ensure separation of concerns, testability, and maintainability:

```text
E_Commerce/
├── E_Commerce.Api/            # Presentation Layer (Controllers, Endpoints, Middlewares)
├── E_Commerce.Application/    # Business Logic, Commands/Queries (CQRS), Interfaces, DTOs
├── E_Commerce.Domain/         # Domain Entities, Value Objects, Enums, Domain Events
└── E_Commerce.Infrastructure/ # Data Access (EF Core), Redis Caching, External Services

🚀 Technologies & Libraries Used
Framework: .NET 8 / .NET 9 (C#)

Architecture: Clean Architecture & CQRS Pattern

Mediator Pattern: MediatR

Data Access: Entity Framework Core & SQL Server

Caching: Distributed Redis Caching

Validation: FluentValidation

Authentication & Authorization: JWT (JSON Web Tokens) & ASP.NET Core Identity

Payment Processing: Stripe API Integration

Object Mapping: AutoMapper / Mapster

✨ Features
Authentication & User Management: Secure Register, Login, Token Refresh, and Role-Based Access Control (RBAC).

Product Catalog: Categories, Products management, Filtering, Sorting, and Pagination.

Shopping Cart & Redis: High-performance cart management using Redis caching.

Order Processing: Order creation, status tracking, and payment processing via Stripe.

Global Error Handling: Custom middleware for consistent API responses.

🛠 Getting Started
Prerequisites
.NET SDK (Version 8.0 or later)

SQL Server

Redis (Local instance or Docker container)

Installation & Setup
Clone the Repository:

Bash
git clone [https://github.com/Ahmed03555/E_Commerce.git](https://github.com/Ahmed03555/E_Commerce.git)
cd E_Commerce
Configure Database & Connection Strings:
Update the appsettings.json file inside E_Commerce.Api with your SQL Server and Redis connection strings:

JSON
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True;",
    "Redis": "localhost:6379"
  }
}
Apply Database Migrations:
Run the following command to create the database and tables:

Bash
dotnet ef database update --project E_Commerce.Infrastructure --startup-project E_Commerce.Api
Run the Project:

Bash
dotnet run --project E_Commerce.Api
Explore API Documentation:
Open your browser and navigate to https://localhost:XXXX/swagger to test the API endpoints.

👨‍💻 Author
Ahmed Yasser

GitHub: @Ahmed03555

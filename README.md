# products-app-backend

This project is the backend of the **Products App**, developed using **.NET 8** with a **Clean Architecture** and **CQRS** approach. 
It provides endpoints for authentication and product CRUD operations, with pagination, sorting, and filtering support.

![Build Status](https://github.com/edsonbassani/products-app-backend/actions/workflows/dotnet.yml/badge.svg)

![CodeQL](https://github.com/edsonbassani/products-app-backend/actions/workflows/codeql-analysis.yml/badge.svg)

## Tech Stack 
- **.NET 8**
- **Entity Framework Core**  
- **PostgreSQL**
- **FluentValidation**
- **MediatR**  
- **AutoMapper**
- **Swagger**
- **Rebus**  
- **JWT Authentication**

## Pipeline Integrations
- **Build**
- **Code Analysis**
- **CodeQL**

## Configuration and Run

### 1. Repo configuration
```bash
git clone https://github.com/edsonbassani/products-app-backend.git
cd products-app-backend
```

### 2. Database configuration
```bash
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=products-db;Username=postgres;Password=yourpassword"
}
```

### 3. Migrations
```bash
dotnet ef database update
```

### 4. Run
```bash
dotnet run
```

### 5. API
```bash
https://localhost:7181/swagger
```

## Tech Stack
- **Angular 19**
- **RxJS**
- **Bootstrap**  
- **Angular Forms**  
- **JWT Authentication**  
- **Standalone Components**
- 
### Main Endpoints
- Authentication
  - POST /api/auth
    - Requires email and password.
    - Returns a JWT token.

### Products
- **GET** `/api/products`  
  - Product listing with pagination, sorting, and filters.
- **POST** `/api/products`  
  - Creation of a new product.
- **PUT** `/api/products/{id}`  
  - Updating an existing product.
- **DELETE** `/api/products/{id}`  
  - Deactivating a product.


### Project Structure
  - Domain: Contains the entities and specifications.
  - Application: Implements use cases with CQRS and MediatR.
  - Infrastructure: Database access and external settings.
  - WebApi: Exposure of application endpoints.

### Tests
  - Some unit tests have been implemented to validate key use cases.

### Pipeline CI/CD
  - The repository contains a basic pipeline configured in GitHub Actions for build automation and testing.


## Building a sample

Build any .NET Core sample using the .NET Core CLI, which is installed with [the .NET Core SDK](https://www.microsoft.com/net/download). Then run
these commands from the CLI in the directory of any sample:

```console
dotnet build
dotnet run
```

These will install any needed dependencies, build the project, and run
the project respectively.

Multi-project samples have instructions in their root directory in
a `README.md` file.  

Except where noted, all samples build from the command line on
any platform supported by .NET Core. There are a few samples that are
specific to Visual Studio and require Visual Studio 2017 or later. In
addition, some samples show platform-specific features and will require
a specific platform. Other samples and snippets require the .NET Framework
and will run on Windows platforms, and will need the Developer Pack for
the target Framework version.


## Screenshots 

![image](https://github.com/user-attachments/assets/8588f347-2af5-4e91-9055-33d93dcdb629)

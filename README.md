# Introduction 
This is an API project for the Employee Management Portal for building .NET-based APIs following Clean Architecture principles. 

It demonstrates best practices in software architecture, testing, and development workflows. 

The project can be used as a starting point for new API development, showcasing:

- Clean Architecture implementation with proper separation of concerns
- Comprehensive testing strategy including unit, integration, and infrastructure tests
- Docker-based integration testing
- Code quality enforcement through pre-commit hooks
- Centralized package and build management
- Modern .NET development practices

# EmployeeManagement - Clean Architecture Solution

This is a .NET solution following Clean Architecture principles, implementing the Employee Management APIs with proper separation of concerns and best practices.

## Solution Structure

The solution is organized into the following layers:

### 1. Domain Layer (`EmployeeManagement.Domain`)
- Contains enterprise business rules and entities
- No dependencies on other layers
- Contains:
  - Entities: Core business objects
  - Common: Shared base classes and interfaces

### 2. Application Layer (`EmployeeManagement.Application`)
- Contains application business rules
- Implements use cases
- Contains:
  - Interfaces: Contracts for infrastructure services
  - Models: DTOs and view models
  - Services: Application services and business logic
  - Exceptions: Custom application exceptions

### 3. Infrastructure Layer (`EmployeeManagement.Infrastructure`)
- Implements interfaces defined in the Application layer
- Contains external concerns like:
  - Email services
  - File storage
  - External API integrations
- No direct dependencies on the Persistence layer

### 4. Persistence Layer (`EmployeeManagement.Persistence`)
- Implements data access concerns
- Contains:
  - DbContext configurations
  - Entity configurations
  - Migrations

### 5. API Layer (`EmployeeManagement.Api`)
- Entry point of the application
- Handles HTTP requests
- Contains:
  - Minimal API Endpoints
  - Middleware configurations
  - API documentation using swagger

## Build Configuration

### Directory.Build.props
The solution uses a central build configuration file (`Directory.Build.props`) that applies common settings across all projects:

```xml
<PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>    
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <AnalysisLevel>latest</AnalysisLevel>
    <AnalysisMode>all</AnalysisMode>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <CodeAnalysisTreatWarningsAsErrors>true</CodeAnalysisTreatWarningsAsErrors>
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
</PropertyGroup>
```

Common build settings explained:
- `TreatWarningsAsErrors`: Treats all warnings as errors, ensuring high code quality
- `Nullable`: Enables nullable reference types for better null safety
- `AnalysisLevel` and `AnalysisMode`: Configures code analysis rules
- `EnforceCodeStyleInBuild`: Enforces code style during build
- `ImplicitUsings`: Enables implicit using directives for common namespaces

## Package Management

The solution uses Central Package Management (CPM) through `Directory.Packages.props`. To add a new package:

1. Open `Directory.Packages.props`
2. Add a new `PackageVersion` entry in the `ItemGroup`:
```xml
<PackageVersion Include="PackageName" Version="x.x.x" />
```

3. Reference the package in your project's `.csproj` file:
```xml
<PackageReference Include="PackageName" />
```

## Code Quality

### Pre-commit Hook
The solution includes a pre-commit hook to ensure code quality. To set it up:

1. Copy the pre-commit hook to your git hooks folder:
```bash
cp .git/hooks/pre-commit.sample .git/hooks/pre-commit
```

2. Add the following content to the pre-commit hook:
```bash
#!/bin/sh

# Run dotnet format to check code style
dotnet format --verify-no-changes || exit 1
```

3. Make the hook executable:
```bash
chmod +x .git/hooks/pre-commit
```

If the pre-commit hook fails, run the following command to fix code style issues:
```bash
dotnet format
```

## Testing

The solution includes multiple test projects:
- `EmployeeManagement.UnitTests`: Unit tests for business logic
- `EmployeeManagement.ArchitectureTests`: Tests for whether the architecture adheres to Clean Architecture principles
- `EmployeeManagement.IntegrationTests`: Integration tests for component interactions and database operations

### Running Tests
Before raising a PR, ensure all tests pass successfully. The following test suites must be executed and pass:

1. Unit Tests: Tests individual components and business logic in isolation
2. Integration Tests: Tests component interactions and database operations
3. Architecture Tests: Tests external service integrations and architecture components

All test suites must pass before submitting a pull request to maintain code quality and prevent regressions.

### Integration Testing with Docker
The integration tests use Docker containers for database testing. Before running integration tests:

1. Install Docker Desktop from https://www.docker.com/products/docker-desktop
2. Ensure Docker Desktop is running
3. The `IntegrationTestWebAppFactory` class sets up a SQL Server container for each test run
4. The container is automatically created and disposed of for each test
5. Database migrations are automatically applied to the test database

Example usage in tests:
```csharp
public class EmployeeServiceTests : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IntegrationTestWebAppFactory _factory;
    
    public EmployeeServiceTests(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
    }
    
    // Test methods...
}
```

## Getting Started

Prerequisites:
1. .NET 8.0 SDK
2. Docker Desktop (for integration tests)
3. Git
4. Visual Studio 2022 or VS Code with C# extensions

Setup steps:
1. Clone the repository
2. Install required tools:
   - Docker Desktop from https://www.docker.com/products/docker-desktop
   - .NET Format tool: `dotnet tool install -g dotnet-format`
3. Set up Git hooks:
   - Copy pre-commit hook to `.git/hooks/pre-commit`
   - Make it executable: `chmod +x .git/hooks/pre-commit`
4. Restore NuGet packages
5. Copy & paste `appsettings.Development.json.dist` & name it `appsettings.Development.json` and update settings
6. Database migrations will be automatically applied during local development when the application starts
7. Run the application

## Git Configuration

### Local Git Setup
Before starting development, configure your local Git settings to ensure correct author information:

```bash
# Set your name for this repository
git config user.name "Your Name"

# Set your email for this repository
git config user.email "your.email@example.com"

# Verify your settings
git config --list
```

### Common Git Commands
```bash
# Clone the repository
git clone <repository-url>

# Create and switch to a new branch
git checkout -b feature/your-feature-name

# Stage changes
git add .

# Commit changes
git commit -m "Your commit message"

# Push changes to remote
git push origin feature/your-feature-name

# Pull latest changes
git pull origin main

# Switch branches
git checkout branch-name

# View status
git status

# View commit history
git log
```

## Development Guidelines

1. Follow Clean Architecture principles
2. Write unit tests for new features
3. Use dependency injection
4. Implement proper validation
5. Follow SOLID principles
6. Use async/await for I/O operations
7. Implement proper error handling
8. Run all tests before committing changes
9. Ensure code formatting passes the pre-commit hook

## Dependencies

Key packages used in the solution:
- FluentValidation: For input validation
- Entity Framework Core: For data access
- Serilog: For logging
- Swagger: For API documentation
- xUnit: For testing
- NSubstitute: For mocking in tests
- Testcontainers: For Docker-based integration testing

## Auth0 API Registration

### Step 1: Auth0 Setup (Manual Configuration in Auth0 Dashboard)

To integrate Auth0 with this API, follow these steps to set up an API in the Auth0 Dashboard:

1. Create an API in Auth0:
   - Navigate to the APIs section in the Auth0 Dashboard.
   - Click on Create API.
   - Fill in the following details:
     - Name: EmployeeManagementPortalAPI
     - Identifier: https://employeemanagementportal/api
     - Signing Algorithm: RS256
   - Click Create.

2. Note the API Identifier:
   - The API Identifier will be used in the appsettings.Development.json.

### Step 2: Application Configuration

Once the API is set up in Auth0, update your application settings to use the Auth0 API:

1. Open appsettings.Development.json and configure the following:
   - Add Auth0 domain vaule in the Domain key under Auth0 object.
   - Add Audience value as https://employeemanagementportal/api under Auth0 object.

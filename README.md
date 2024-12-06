# AssetManagement - Rookeie To Engineer Batch 7 - NashTech 2024
 ![image](https://github.com/user-attachments/assets/72d533b5-68c0-4518-af8f-dca6ae7c5e53)
The Asset Management System is a web application designed to streamline the management of assets and users within an organization. Built with a focus on security, scalability, and clean architecture principles, this system ensures efficient operations and role-based access control. 
# Features: 
## Authentication & Authorization
+ Secure user authentication using cookies.
+ Role-based authorization to manage access levels effectively.
## Asset Management
+ Create and register new assets in the system.
+ Browse and view the list of available or assigned assets.
+ Handle asset returns with a seamless process.
## User Management
+ Create and manage user profiles efficiently.
+ Enable user deletion with appropriate access permissions.
## Clean Architecture
+ Implements the Clean Architecture pattern for maintainable and scalable code.
+ Separation of concerns ensures flexibility and ease of testing.

# Technology Stack
Backend: ASP.NET Core, Entity Framework
Frontend: React.js, Redux Toolkit
Database: SQL Server
Authentication: Cookie-based Authentication
 
## Prerequisite

Install **.NET Core User Secrets** extension via visual studio code
right click on **Web.csproj** then click **Manage User Secrets**
paste below configuration (add your sql credential to file)

```json for cloud database
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:sql-net-b7-1.database.windows.net,1433;Initial Catalog=assetmanagement-local;Persist Security Info=False;User ID=<<SQL_USER_NAME>>;Password=<<SQL_PASSWORD>>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

```json for local database
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=database_name;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

## Build

Run `dotnet build -tl` to build the solution.

## Run

To run the web application:

```bash
cd .\src\Web\
dotnet watch run
```

To access swagger:
http://localhost:5000/swagger/index.html

Navigate to https://localhost:44447/. The application will automatically reload if you change any of the source files.

## Code Styles & Formatting

The template includes [EditorConfig](https://editorconfig.org/) support to help maintain consistent coding styles for multiple developers working on the same project across various editors and IDEs. The **.editorconfig** file defines the coding styles applicable to this solution.

## Test

The solution contains unit, integration, functional, and acceptance tests.

To run the unit, integration, and functional tests (excluding acceptance tests):

```bash
dotnet test --filter "FullyQualifiedName!~AcceptanceTests"
```

## Help

To learn more about the template go to the [project website](https://github.com/jasontaylordev/CleanArchitecture). Here you can find additional guidance, request new features, report a bug, and discuss the template with other users.

## right copy

All right belong to team 1 include Binh, Kien, Trieu, Van, Phuong

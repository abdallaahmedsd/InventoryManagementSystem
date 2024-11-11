
# Inventory Management System

An Inventory Management System built with ASP.NET Core and Entity Framework Core, providing RESTful APIs for managing items and categories.

## Prerequisites

To run this application, you will need the following:

- **.NET SDK**: Version 8.0 or later
- **SQL Server**: You can use a local or remote SQL Server instance

## Getting Started

Follow these steps to set up and run the application.

### 1. Clone the Repository

Clone the repository to your local machine using Git:

```bash
git clone https://github.com/abdallaahmedsd/InventoryManagementSystem.git
```

### 2. Configure the Database Connection

Open the `appsettings.json` file located in the root of the project.  
Update the connection string under `"ConnectionStrings": { "DefaultConnection": "Your_SQL_Server_Connection_String" }` to match your SQL Server configuration.

### 3. Build the Application

Navigate to the project directory and build the application:

```bash
dotnet build
```

### 4. Run the Application

Simply run the application to initialize the database and start the server. The application is configured to check for any pending migrations on startup and apply them automatically if needed.

```bash
dotnet run
```

### 5. Access the Application

Once the application is running, you can access the API endpoints using a tool like [Postman](https://www.postman.com/) or directly in a browser at:

```
http://localhost:5146/api/items
http://localhost:5146/api/categories
```

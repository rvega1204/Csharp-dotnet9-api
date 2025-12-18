# FirstAPI

A RESTful API built with ASP.NET Core 9.0 for managing a book collection.

## Project Overview

FirstAPI is a simple CRUD (Create, Read, Update, Delete) API that demonstrates modern ASP.NET Core development practices with Entity Framework Core and MySQL database integration.

## Features

- RESTful API endpoints for book management
- Entity Framework Core with MySQL database
- Comprehensive unit testing with xUnit
- In-memory database testing
- XML documentation for API endpoints

## Technology Stack

- **.NET 9.0** - Target framework
- **ASP.NET Core** - Web API framework
- **Entity Framework Core 9.0.4** - ORM
- **MySQL** - Database (via Pomelo.EntityFrameworkCore.MySql 9.0.0)
- **xUnit** - Testing framework
- **Moq 4.20.72** - Mocking library
- **FluentAssertions 8.8.0** - Assertion library
- **Microsoft.EntityFrameworkCore.InMemory 9.0.4** - In-memory database for testing

## Project Structure

```
firstApi/
├── FirstAPI/                          # Main API project
│   ├── Controllers/
│   │   ├── BooksController.cs        # Book CRUD endpoints
│   │   └── WeatherForecastController.cs
│   ├── Data/
│   │   └── FirstAPIContext.cs        # Database context
│   ├── Models/
│   │   ├── Book.cs                   # Book entity
│   │   └── WeatherForecast.cs
│   ├── Migrations/                   # EF Core migrations
│   └── Program.cs                    # Application entry point
│
├── FirstAPI.Tests/                   # Test project
│   ├── Controllers/
│   │   └── BooksControllerTests.cs  # Controller unit tests (12 tests)
│   └── Models/
│       └── BookTests.cs             # Model unit tests (6 tests)
│
├── FirstAPI.sln                      # Solution file
└── README.md                         # This file
```

## API Endpoints

### Books

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Books` | Retrieve all books |
| GET | `/api/Books/{id}` | Retrieve a specific book by ID |
| POST | `/api/Books` | Create a new book |
| PUT | `/api/Books/{id}` | Update an existing book |
| DELETE | `/api/Books/{id}` | Delete a book |

### Book Model

```json
{
  "id": 1,
  "title": "Book Title",
  "author": "Author Name",
  "yearPublished": 2024
}
```

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/rvega1204/Csharp-dotnet9-api.git
cd firstApi
```

2. Create a `.env` file in the project root with your database configuration:
```bash
cp .env.example .env
```

Then edit the `.env` file with your database credentials:
```env
DB_SERVER=localhost
DB_PORT=3306
DB_NAME=firstapi_data
DB_USER=root
DB_PASSWORD=your_password_here
```

**Note:** The `.env` file is already in `.gitignore` and will not be committed to version control.

3. Apply database migrations:
```bash
cd FirstAPI
dotnet ef database update
```

4. Run the application:
```bash
dotnet run
```

The API will be available at `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP).

## Configuration

### Environment Variables

The application uses a `.env` file for configuration. The following environment variables are supported:

| Variable | Description | Default |
|----------|-------------|---------|
| `DB_SERVER` | MySQL server address | `localhost` |
| `DB_PORT` | MySQL server port | `3306` |
| `DB_NAME` | Database name | `firstapi_data` |
| `DB_USER` | Database username | `root` |
| `DB_PASSWORD` | Database password | (required) |

The `.env.example` file provides a template for your configuration.

## Running Tests

Run all tests in the solution:
```bash
dotnet test
```

Run tests with detailed output:
```bash
dotnet test --verbosity normal
```

Run tests with code coverage:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Test Coverage

The project includes 18 comprehensive unit tests:

**BooksController Tests (12 tests)**
- ✅ GetBooks returns list of books
- ✅ GetBooks returns empty list when no books exist
- ✅ GetBookById returns specific book
- ✅ GetBookById returns NotFound for non-existent book
- ✅ AddBook creates new book successfully
- ✅ AddBook returns BadRequest for null book
- ✅ UpdateBook updates existing book
- ✅ UpdateBook returns NotFound for non-existent book
- ✅ DeleteBook removes book successfully
- ✅ DeleteBook returns NotFound for non-existent book

**Book Model Tests (6 tests)**
- ✅ Book can be created with all properties
- ✅ Book properties can be set individually
- ✅ Book default ID is zero
- ✅ Book can be created with different values (Theory test)
- ✅ Book year can be negative (for historical books)

## Database Migrations

Create a new migration:
```bash
cd FirstAPI
dotnet ef migrations add MigrationName
```

Update the database:
```bash
dotnet ef database update
```

Revert the last migration:
```bash
dotnet ef database update PreviousMigrationName
```

Remove the last migration:
```bash
dotnet ef migrations remove
```

## Development

### Building the Project

```bash
dotnet build
```

### Restoring Dependencies

```bash
dotnet restore
```

### Adding New Packages

To the main project:
```bash
cd FirstAPI
dotnet add package PackageName
```

To the test project:
```bash
cd FirstAPI.Tests
dotnet add package PackageName
```

## API Documentation

The API includes XML documentation for all endpoints. When running in Development mode, you can access the API documentation through Swagger/OpenAPI at:

```
https://localhost:5001/swagger
```

## Example Requests

### Get All Books
```bash
curl -X GET https://localhost:5001/api/Books
```

### Get Book by ID
```bash
curl -X GET https://localhost:5001/api/Books/1
```

### Create a New Book
```bash
curl -X POST https://localhost:5001/api/Books \
  -H "Content-Type: application/json" \
  -d '{
    "title": "The Great Gatsby",
    "author": "F. Scott Fitzgerald",
    "yearPublished": 1925
  }'
```

### Update a Book
```bash
curl -X PUT https://localhost:5001/api/Books/1 \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "title": "The Great Gatsby - Updated",
    "author": "F. Scott Fitzgerald",
    "yearPublished": 1925
  }'
```

### Delete a Book
```bash
curl -X DELETE https://localhost:5001/api/Books/1
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Author

Created as part of a C# learning course, Ricardo Vega 2025.

## Acknowledgments

- ASP.NET Core documentation
- Entity Framework Core documentation
- xUnit testing framework
- FluentAssertions library

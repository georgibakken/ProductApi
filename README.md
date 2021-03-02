# ProductApi

Simple REST API for Products, using ASP.NET Core 5.0

## Development setup
### Prerequisites
- [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)

### Setup
1. Clone the repository
2. Open the `ProductApi.sln` file in your favourite IDE
3. Run the application
4. Visit `http://localhost:5001/api/products/` in your browser

### Docker
1. Run `docker build -t aspnetapp .` from the root directory
2. Run `docker run -it --rm -p 5001:80 aspnetapp`
3. Visit `http://localhost:5001/api/products/` in your browser

## Running tests
1. `cd ProductApi.Tests`
2. `dotnet test`

## API Documentation
After running the application, visit `https://localhost:5001/swagger/` to get an overview over available services.
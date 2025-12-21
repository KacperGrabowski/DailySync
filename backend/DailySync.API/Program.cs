using DailySync.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Adding DbContext

string connectionString = builder.Configuration.GetConnectionString("DailySyncDb")
                            ?? throw new ArgumentNullException("The Database connection string is missing!");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddOpenApi();

WebApplication app = builder.Build();

// SERVICES - Registration in the Dependency Injection Container

builder.Services.AddControllers();
// Adds support for Controllers (classes decorated with [ApiController])
// Without this, your API endpoints will not work!
// Required for: AuthController, ReceiptsController, etc.

builder.Services.AddEndpointsApiExplorer();
// Enables Swagger to "discover" your endpoints
// Scans Controllers and generates API documentation
// Without this, Swagger won't detect your methods

builder.Services.AddSwaggerGen();
// Generates OpenAPI (Swagger UI) documentation
// Provides an interface to test your API in the browser
// http://localhost:5000/swagger


// MIDDLEWARE PIPELINE - Order MATTERS!

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Serves the API definition JSON (openapi.json)

    app.UseSwaggerUI();
    // Serves the Swagger UI interface
    // ONLY in Development (not in Production!)
}

app.UseHttpsRedirection();
// Redirects HTTP -> HTTPS (security)
// http://localhost:5000 -> https://localhost:5001

app.UseAuthorization();
// Checks if the user is authorized to access endpoints
// Required for [Authorize] attributes

app.MapControllers();
// Maps Controllers to the routing system
// Without this, your [Route] attributes won't work
// This MUST be at the end!


app.Run();
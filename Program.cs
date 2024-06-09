using Backend.Interview.Api.ApplicationCore.Contracts;
using Backend.Interview.Api.Infrastructure;
using Backend.Interview.Api.Infrastructure.Data;
using Backend.Interview.Api.Infrastructure.Repository;
using Backend.Interview.Api.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure user secrets
builder.Configuration.AddUserSecrets<Program>();

// Retrieve username and password from user secrets
var username = builder.Configuration["DbUsername"];
var password = builder.Configuration["DbPassword"];

// Replace placeholders in the connection string
var postgresConnectionStringTemplate = "Host=addressbook-db.postgres.database.azure.com;Database=postgres;Username={username};Password={password};SslMode=Require;Trust Server Certificate=true";
var connectionString = postgresConnectionStringTemplate.Replace("{username}", username).Replace("{password}", password);

// Add services to the container.
ConfigureServices(builder.Services, connectionString);

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, string connectionString)
{
    services.AddDbContext<BackendInterviewDbContext>(options => options.UseNpgsql(connectionString));

    services.AddScoped<IPersonService, PersonService>();
    services.AddScoped<IPersonRepository, PersonRepository>();

    services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("*")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors();
    app.UseAuthorization();
    app.MapControllers();
}

using Backend.Interview.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Retrieve username and password from user secrets
var username = builder.Configuration["DbUsername"];
var password = builder.Configuration["DbPassword"];

// Replace placeholders in the connection string
var PostgresAzure =
    "Host=addressbook-db.postgres.database.azure.com;Database=postgres;Username={username};Password={password};SslMode=Require;Trust Server Certificate=true";

var connectionString = PostgresAzure.Replace("{username}", username)
    .Replace("{password}", password);

// Add services to the container.
builder.Services.AddDbContext<BackendInterviewDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

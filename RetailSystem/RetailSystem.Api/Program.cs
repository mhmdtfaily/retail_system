using Microsoft.EntityFrameworkCore;
using RetailSystem.Application.Interfaces;
using RetailSystem.Infrastructure;
using RetailSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext for SQL LocalDB
builder.Services.AddDbContext<RetailDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RetailDbConnection")));


// Register the repository
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

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

using FluentValidation;
using FluentValidation.AspNetCore;
using JoaquinOrder.Clients;
using JoaquinOrder.Data;
using JoaquinOrder.Middleware;
using JoaquinOrder.Models;
using JoaquinOrder.Repositories;
using JoaquinOrder.Services;
using JoaquinOrder.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/order-service-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IValidator<Order>, OrderValidator>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<ProductClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5000");
});

builder.Services.AddHttpClient<CustomerClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5001");
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting JoaquinOrder microservice");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "JoaquinOrder microservice terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
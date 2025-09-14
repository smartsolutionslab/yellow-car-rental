using System.Runtime.CompilerServices;
using Microsoft.OpenApi.Models;
using SmartSolutionsLab.YellowCarRental.Api;
using SmartSolutionsLab.YellowCarRental.Application;
using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;
using SmartSolutionsLab.YellowCarRental.Domain;
using SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

builder.RegisterAllApplicationCommandsAndHandlers();
builder.AddPersistence();



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Yellow Car Rental API", Version = "v1" });
});

// add a CORS policy for the client
// TODO: maybe solvable by default aspnet.core implementation
var allowedCorsOrigins = builder.Configuration.GetValue<string>("AllowedCorsOrigins")?
                             .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                         ?? new[] { "https://localhost:5059", "https://localhost:7123", "" };
builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins(allowedCorsOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

var app = builder.Build();
app.Services.UsePersistence(app.Configuration);

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseCors("wasm"); 

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.MapVehicleEndpoints();
app.MapStationEndpoints();
app.MapBookingsEndpoints();
app.MapApiCustomerEndpoints();


app.MapDefaultEndpoints();



app.MapSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "My API V1");
});

app.Run();




using BeerInventory.Data;
using BeerInventory.Data.Repositories;
using BeerInventory.Models;
using BeerInventory.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BeerContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<BeerService>();
builder.Services.AddScoped<BreweryService>();
builder.Services.AddScoped<BarService>();
builder.Services.AddScoped<BreweryBeerService>();
builder.Services.AddScoped<BarBeerService>();
builder.Services.AddScoped<IBeerRepository, BeerRepository>();
builder.Services.AddScoped<IBreweryRepository, BreweryRepository>();
builder.Services.AddScoped<IBarRepository, BarRepository>();
builder.Services.AddScoped<IBreweryBeerRepository, BreweryBeerRepository>();
builder.Services.AddScoped<IBarBeerRepository, BarBeerRepository>();

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

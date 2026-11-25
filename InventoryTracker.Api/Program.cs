using InventoryTracker.Data;
using InventoryTracker.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<InventoryTrackerDbContext>(options =>
    options.UseSqlite(DatabaseSettings.GetConnectionString()));

var app = builder.Build();

// Seed the database when explicitely enabled
if (app.Environment.IsDevelopment() &&
    app.Configuration.GetValue<bool>("SeedSampleData"))
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider
        .GetRequiredService<InventoryTrackerDbContext>();

    await DbSeeder.SeedAsync(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{

}
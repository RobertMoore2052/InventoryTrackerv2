using System.Data.Common;
using InventoryTracker.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace InventoryTracker.Tests;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<
                IDbContextOptionsConfiguration<InventoryTrackerDbContext>>();

            services.AddSingleton<DbConnection>(_ =>
            {
                var connection =
                    new SqliteConnection("DataSource=:memory:");

                connection.Open();

                return connection;
            });

            services.AddDbContext<InventoryTrackerDbContext>(
                (ServiceProvider, options) =>
                {
                    var connection =
                        ServiceProvider.GetRequiredService<DbConnection>();

                    options.UseSqlite(connection);
                });
        });

        builder.UseEnvironment("Testing");
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using var scope = host.Services.CreateScope();

        var context =
            scope.ServiceProvider
                .GetRequiredService<InventoryTrackerDbContext>();

        context.Database.EnsureCreated();

        return host;
    }
}
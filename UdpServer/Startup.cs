using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UdpServer.Core.Data.Source.Local.Dal.DataContext;
using Microsoft.EntityFrameworkCore;
using UdpServer;

string connectionString = "Data Source=(localdb)\\MSSqlLocalDb;Database=BooksStore";

try
{
    Host.CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
            services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));
            services.AddSingleton<Application>();
        })
        .Build()
        .Services.GetRequiredService<Application>()
        .Start();
}
catch (Exception ex)
{
    Console.WriteLine($"Startup exception: {ex.Message}");
}
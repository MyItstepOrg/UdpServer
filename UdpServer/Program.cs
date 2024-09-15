using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UdpServer.Core.Data.Source.Local.Dal.DataContext;
using Microsoft.EntityFrameworkCore;
using UdpServer;
using UdpServer.Services.Services;

//Connection string
string connectionString = "Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=UdpChatServerDb;Integrated Security=SSPI;";

try
{
    //Initializing app
    Host.CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ChatService>();
            services.AddScoped<UsersService>();
            services.AddScoped<Application>();
        })
        .Build()
        .Services.GetRequiredService<Application>()
        .Start();
}
catch (Exception ex)
{
    Console.WriteLine($"Startup exception: {ex.Message}");
}
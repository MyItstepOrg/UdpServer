using UdpServer.Core.Data.Source.Local.Dal.DataContext;

namespace UdpServer.Services.Services;
public class ClientService(DataContext data)
{
    private readonly DataContext data = data;
}

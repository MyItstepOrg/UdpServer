using System.Net;
using UdpServer.Core.Data.Dto;
using UdpServer.Core.Data.Source.Local.Dal.DataContext;
using UdpServer.Services.Services.Repository;

namespace UdpServer.Services.Services;
public class ClientService(DataContext data) : Repository<ClientDto>(data)
{
    public ClientDto? Get(int id) => this.Find(c => c.Id == id);
    public ClientDto? GetByUser(string username) => this.Find(c => c.Username == username);
    public ClientDto? GetByIp(IPEndPoint ip) => this.Find(c => c.Address == ip);
}

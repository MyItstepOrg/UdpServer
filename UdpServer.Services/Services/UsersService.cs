using System.Net;
using UdpServer.Core.Data.Dto;
using UdpServer.Core.Data.Source.Local.Dal.DataContext;
using UdpServer.Services.Services.Repository;

namespace UdpServer.Services.Services;
public class UsersService(DataContext data) : Repository<UserDto>(data)
{
    public UserDto? Get(int id) => this.Find(c => c.Id == id);
    public UserDto? GetByUser(string username) => this.Find(c => c.Username == username);
    public UserDto? GetByIp(IPEndPoint ip) => this.Find(c => c.Address == ip);
}

using UdpServer.Core.Data.Dto;
using UdpServer.Core.Data.Source.Dal.DataContext;
using UdpServer.Services.Services.DataAccess.Repository;

namespace UdpServer.Services.Services.DataAccess;
public class UsersService(DataContext data) : Repository<UserDto>(data)
{
    public UserDto? Get(int id) => Find(c => c.Id == id);
}

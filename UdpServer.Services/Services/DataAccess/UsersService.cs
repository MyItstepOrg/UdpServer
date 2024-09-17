using System.Diagnostics.CodeAnalysis;
using UdpServer.Core.Data.Dto;
using UdpServer.Core.Data.Source.Dal.DataContext;
using UdpServer.Services.Services.DataAccess.Repository;

namespace UdpServer.Services.Services.DataAccess;
public class UsersService(DataContext data) : Repository<UserDto>(data)
{
    public UserDto? Get(int id) => Find(c => c.Id == id);
    public void Update(int id, [DisallowNull] UserDto updatedUser)
    {
        UserDto userToUpdate = Get(id);
        if (userToUpdate != null)
        {
            userToUpdate.Username = updatedUser.Username;
            userToUpdate.IpAddress = updatedUser.IpAddress;
            userToUpdate.Port = updatedUser.Port;
        }

        UpdateItem(userToUpdate);
    }
}

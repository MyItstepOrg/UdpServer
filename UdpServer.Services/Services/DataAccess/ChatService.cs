using UdpServer.Core.Data.Dto;
using UdpServer.Core.Data.Source.Dal.DataContext;
using UdpServer.Services.Services.DataAccess.Repository;

namespace UdpServer.Services.Services.DataAccess;
public class ChatService(DataContext data) : Repository<ChatDto>(data)
{
    public ChatDto? Get(int id) => Find(c => c.Id == id);
    public List<ChatDto> GetChatsForUser(UserDto user) => FindAll(c => c.UsersList.Contains(user));
}

using UdpServer.Core.Data.Dto;
using UdpServer.Core.Data.Source.Local.Dal.DataContext;
using UdpServer.Services.Services.Repository;

namespace UdpServer.Services.Services;
public class ChatService(DataContext data) : Repository<ChatDto>(data)
{
    public ChatDto? GetById(int id) => this.Find(c => c.Id == id);
}

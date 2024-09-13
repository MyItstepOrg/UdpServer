using UdpServer.Core.Data.Dto;
using UdpServer.Core.Data.Source.Local.Dal.DataContext;
using UdpServer.Services.Services.Repository;

namespace UdpServer.Services.Services;
public class GroupService(DataContext data) : Repository<GroupDto>(data)
{
    public GroupDto? Get(int id) => this.Find(c => c.Id == id);
    public GroupDto? GetByName(string name) => this.Find(g => g.Name == name);
}

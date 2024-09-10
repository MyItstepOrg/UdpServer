using Microsoft.EntityFrameworkCore;
using UdpServer.Core.Data.Dto;

namespace UdpServer.Core.Data.Source.Local.Dal.DataContext;
public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ClientDto> Clients { get; set; }
    public DbSet<ChatDto> Chats { get; set; }
    public DbSet<GroupDto> Groups { get; set; }
}

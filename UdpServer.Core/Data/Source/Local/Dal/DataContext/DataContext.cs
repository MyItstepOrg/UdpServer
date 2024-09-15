using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UdpServer.Core.Data.Dto;

namespace UdpServer.Core.Data.Source.Local.Dal.DataContext;
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<ChatDto> Chats { get; set; }
}
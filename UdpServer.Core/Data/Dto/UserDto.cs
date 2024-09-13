using System.Net;

namespace UdpServer.Core.Data.Dto;
public class UserDto
{
    public uint Id { get; set; }
    public required IPEndPoint Address { get; set; }
    public string? Username { get; set; }
}

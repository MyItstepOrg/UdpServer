using System.Net;

namespace UdpServer.Core.Data.Dto;
public class UserDto
{
    public int Id { get; set; }
    public string? Username { get; set; }

    public string? IpAddress { get; set; }
    public int Port { get; set; }

    // Not mapped to database, just for convenience
    public IPEndPoint Address
    {
        get => new IPEndPoint(IPAddress.Parse(this.IpAddress), this.Port);
    }
}

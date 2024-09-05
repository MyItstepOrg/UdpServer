using System.Net;

namespace UdpServer.Core.Data.Dto;
public class ClientDto
{
    public uint Id { get; set; }
    public required IPEndPoint Address { get; set; }
    public string? Username { get; set; }
}

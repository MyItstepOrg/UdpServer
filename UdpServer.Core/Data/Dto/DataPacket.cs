namespace UdpServer.Core.Data.Dto;
public class DataPacket
{
    public int UserId { get; set; }
    public string? Username { get; set; } = string.Empty;
    public string? Command { get; set; } = string.Empty;
    public string? Content { get; set; } = string.Empty;
}
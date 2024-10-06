namespace UdpServer.Core.Data.Dto;
public class DataPacket
{
    public required string PacketType { get; set; }
    public int SenderId { get; set; }
    public DateTime TimeStamp { get; set; }
    public required List<object> Payload { get; set; }
}
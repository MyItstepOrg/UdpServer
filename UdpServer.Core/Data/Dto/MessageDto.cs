namespace UdpServer.Core.Data.Dto;
public class MessageDto
{
    public uint Id { get; set; }
    public DateTime Time {  get; set; }
    public string? Content { get; set; }
    public string? Sender { get; set; }
}

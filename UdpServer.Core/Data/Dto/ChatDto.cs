namespace UdpServer.Core.Data.Dto;
public class ChatDto
{
    public uint Id { get; set; }
    public string? Name { get; set; }
    public List<MessageDto> MessageHistory { get; set; } = [];
    public List<ClientDto> ClientList { get; set; } = [];
}

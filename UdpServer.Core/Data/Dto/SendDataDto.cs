namespace UdpServer.Core.Data.Dto;
public class SendDataDto
{
    public List<ClientDto> Clients { get; set; } = [];
    public List<ChatDto> Chats { get; set; } = [];
}

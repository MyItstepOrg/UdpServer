namespace UdpServer.Core.Data.Dto;
public class ChatDto
{
    public uint Id { get; set; }
    public string? Name { get; set; }
    public List<MessageDto> MessageHistory { get; set; } = new();
    public List<UserDto> UsersList { get; set; } = new();
}

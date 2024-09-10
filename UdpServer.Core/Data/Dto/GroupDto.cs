namespace UdpServer.Core.Data.Dto;
public class GroupDto
{
    public uint Id { get; set; }
    public string? Name { get; set; }
    public List<ClientDto> ClientList { get; set; } = [];
    public required ChatDto Chat { get; set; }
}

using System.Net;
using UdpServer.Core.Data.Dto;

namespace UdpServer.Services.Services.DataHandlers;
public class DataPacketsHandler(UsersHandler users, ChatsHandler chats)
{
    private readonly UsersHandler users = users;
    private readonly ChatsHandler chats = chats;

    public void HandleCommand(DataPacket dataPacket, IPEndPoint ip)
    {
        try
        {
            switch (dataPacket.PacketType)
            {
                case "/connect":
                    users.RegUser(dataPacket.SenderId, ip);
                    break;
                case "/getchatcontent":

                    break;
                case "/send":
                    break;
                case "/updateuserinfo":
                    break;
                case "/createnewchat":
                    break;
                case "/adduserstochat":
                    break;
                default:
                    throw new Exception("Unknown command!");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Target site: {ex.TargetSite} Exception: {ex.Message}");
        }
    }

}

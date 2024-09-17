using System.Net.Sockets;
using System.Net;
using UdpServer.Services.Services;
using System.Text;
using UdpServer.Core.Data.Dto;
using UdpServer.Services.Services.DataAccess;

namespace UdpServer;

//TODO: Add services
public class Application(ChatService chats, UsersService users)
{
    //Data
    private readonly ChatService chats = chats;
    private readonly UsersService users = users;

    //Data processor
    JsonConverter jsonConverter = new();

    //Server
    public required Udp server;
    IPAddress ip = IPAddress.Parse("127.0.0.1");
    int port = 1025;

    public void Start()
    {
        //Initializing server
        server = new(new IPEndPoint(ip, port));
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Server started...");

            //Begin receiving data
            Receive();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{ex.Source} Exception: {ex.Message}");
        }
        finally
        {
            server.Close();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Server closed.");
        }
    }
    public void Receive()
    {
        Console.WriteLine($"Begin receiving data from port {port}...");
        while (true)
        {
            try
            {
                //Receiving data
                var receive = server.Receive();
                //Decoding received data
                string message = Encoding.Unicode.GetString(receive.Buffer);
                //Deserializing received json from client
                var dataPacket = jsonConverter.Desirialize<DataPacket>(message);
                //TODO: handle commands sent by user
                switch (dataPacket.Command.ToLower())
                {
                    case "#connect":
                        RegUser(dataPacket.UserId, receive.RemoteEndPoint);
                        break;
                    case "#getchats":
                        chats.GetChatsForUser(users.Get(dataPacket.UserId));
                        break;
                    case "#getchatcontent":
                        chats.Get(int.Parse(dataPacket.Content));
                        break;
                    case "#sendtochat":
                        break;
                    case "#updateuserinfo":
                        break;
                    case "#createnewchat":
                        break;
                    case "#adduserstochat":
                        break;
                    default:
                        throw new Exception("Unknown command!");

                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{ex.Source} Exception: {ex.Message}");
            }
        }
    }
    public void RegUser(int id, IPEndPoint ip)
    {
        UserDto newUser = new()
        {
            Id = id,
            Username = "user",
            IpAddress = ip.Address.ToString(),
            Port = ip.Port
        };
        if (users.Get(id) is null)
        {
            users.Add(newUser);
            Console.WriteLine($"New user {newUser.Username + "#" + newUser.Id} registrated!");
        }
        else
        {
            users.Update(id, newUser);
            Console.WriteLine($"{users.Get(id)} connected");
        }
        DataPacket userUpdatePacket = new()
        {
            Command = "#updateuserinfo",
            Content = jsonConverter.Serialize(users.Get(newUser.Id))
        };
        Send(jsonConverter.Serialize(userUpdatePacket), newUser.Id);
    }
    public void Send(string data, int id)
    {
        try
        {
            UserDto user = users.Get(id);
            IPEndPoint userAddress = new (IPAddress.Parse(user.IpAddress), user.Port);
            if (!server.Send(data, userAddress))
                throw new Exception("Unable to send! User is Unreachable!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Data has been succesfuly sent to {userAddress}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to send data!");
            Console.WriteLine($"{ex.Source} Exception: {ex.Message}");
        }
    }
    public void SendToChat(int chatId)
    {

    }
}

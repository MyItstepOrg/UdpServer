using System.Net.Sockets;
using System.Net;
using UdpServer.Services.Services;
using UdpServer.Core.Data.Source.Remote;
using System.Text;
using System.Text.Json;
using UdpServer.Core.Data.Dto;

namespace UdpServer;

//TODO: Add services
public class Application(ChatService chats, UsersService users)
{
    //Data
    private readonly ChatService chats = chats;
    private readonly UsersService users = users;

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
            Console.WriteLine("Server started...");

            chats.Add(new UdpServer.Core.Data.Dto.ChatDto()
            {
                Name = "Main"
            });

            //Begin receiving data
            Receive();
        }
        catch (SocketException sockEx)
        {
            Console.WriteLine($"Socket exception: {sockEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        finally
        {
            server.Close();
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
                //Packing data to json and sending them to client
                if (message.ToLower() == "#connect")
                {
                    Console.WriteLine($"{receive.RemoteEndPoint} connected!");
                    RegUser(receive.RemoteEndPoint);
                    Console.WriteLine($"{users.GetByIp(receive.RemoteEndPoint)} connected!");
                    Send(
                        ConvertToJson(
                            chats.FindAll(
                                c => c.UsersList.Contains(
                                    users.GetByIp(receive.RemoteEndPoint)))), receive.RemoteEndPoint);
                }
                else
                {
                    chats.GetByName("Main")
                        .MessageHistory
                        .Add(new MessageDto()
                        {
                            Time = DateTime.Now,
                            Content = message,
                            Sender = users.GetByIp(receive.RemoteEndPoint).Username
                        });
                    foreach (var c in chats.GetByName("Main").UsersList)
                        if (c.Address != receive.RemoteEndPoint)
                            Send(message, c.Address);
                }
            }
            catch (SocketException sockEx)
            {
                Console.WriteLine($"Socket exception: {sockEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
    public void Send(string data, IPEndPoint address)
    {
        server.Send(data, address);
        try
        {
            if (!server.Send(data, address))
                throw new Exception("Unable to send");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Data has been succesfuly sent to {users.GetByIp(address)}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send data!");
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
    public void RegUser(IPEndPoint ip)
    {
        if (users.GetByIp(ip) is null)
        {
            users.Add(new UserDto()
            {
                Username = ip.ToString(),
                IpAddress = ip.Address.ToString(),
                Port = ip.Port
            });
        }
        if (!this.chats.GetByName("Main").UsersList.Contains(this.users.GetByIp(ip)))
            this.chats.GetByName("Main").UsersList.Add(new UserDto()
            {
                Username = ip.ToString(),
                IpAddress = ip.Address.ToString(),
                Port = ip.Port
            });
    }
    public string ConvertToJson<T>(T item) => "#info" + JsonSerializer.Serialize(item);
}

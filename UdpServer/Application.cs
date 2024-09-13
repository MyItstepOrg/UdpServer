using System.Net.Sockets;
using System.Net;
using UdpServer.Services.Services;
using UdpServer.Core.Data.Source.Remote;

namespace UdpServer;

//TODO: Add services
public class Application(ChatService chats, ClientService clients, GroupService groups)
{
    private readonly ChatService chats = chats;
    private readonly ClientService clients = clients;
    private readonly GroupService groups = groups;

    public async void Start()
    {
        Udp listener = new(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024));
        Console.WriteLine("Server started...");
        Console.WriteLine("Waiting for clients...\n");

        try
        {
            while (true)
            {
                var receive = await listener.Receive();
                string message = System.Text.Encoding.UTF8.GetString(receive.Buffer);
                Console.WriteLine($"{DateTime.Now} | {receive.RemoteEndPoint}: {message}\n");
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
        finally
        {
            listener.Close();
            Console.WriteLine("Server closed.");
        }
    }
}

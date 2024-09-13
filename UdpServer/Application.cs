using System.Net.Sockets;
using System.Net;
using UdpServer.Services.Services;
using UdpServer.Core.Data.Source.Remote;
using System.Text;
using System.Text.Json;
using UdpServer.Core.Data.Dto;

namespace UdpServer;

//TODO: Add services
public class Application(ChatService chats, ClientService clients)
{
    //Data
    private readonly ChatService chats = chats;
    private readonly ClientService clients = clients;

    //Server
    public required Udp server;

    public void Start()
    {
        //Initializing server
        server = new(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024));
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            //Start receiving data
            Thread threadReceive = new Thread(new ThreadStart(this.Receive));
            threadReceive.IsBackground = true;
            threadReceive.Start();

            //Register clients, that connect to port
            Thread threadRegClients = new Thread(new ThreadStart(this.RegClients));
            threadRegClients.IsBackground = true;
            threadRegClients.Start();

            Console.WriteLine("Server started...");
            Console.WriteLine("Waiting for clients...\n");
            //Console.ForegroundColor = ConsoleColor.Red;
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
    public async void Receive()
    {
        try
        {
            while (true)
            {
                var receive = await server.Receive();
                string message = Encoding.UTF8.GetString(receive.Buffer);
                if (message.ToLower() == "#connect")
                {
                    SendDataDto sendData = new SendDataDto()
                    {
                        Clients = this.clients.GetAll(),
                        Chats = this.chats.FindAll(
                            c => c.ClientList.Contains(this.clients.GetByIp(receive.RemoteEndPoint)))
                    };
                    Send(convertToJson(sendData), receive.RemoteEndPoint);
                }
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
    public void Send(string data, IPEndPoint address)
    {
        //TODO: proper send method
        this.server.Send(data, address);
    }
    public void RegClients()
    {
        //TODO: registrating clients
    }
    public string convertToJson<T>(T item) => JsonSerializer.Serialize(item);
}

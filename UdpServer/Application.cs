using System.Net.Sockets;
using System.Net;
using UdpServer.Services.Services;
using UdpServer.Core.Data.Source.Remote;
using System.Text;
using System.Text.Json;
using System.Diagnostics;

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
            Console.WriteLine("Waiting for clients...\n");

            //Register users
            Thread regUsers = new Thread(new ThreadStart(RegClients));
            regUsers.IsBackground = true;
            regUsers.Start();

            //Begin receiving data
            this.Receive();
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
        try
        {
            while (true)
            {
                //Receiving data
                var receive = server.Receive();
                //Decoding received data
                string message = Encoding.UTF8.GetString(receive.Buffer);
                //Packing data to json and sending them to client
                Debug.WriteLine(message);
                if (message.ToLower() == "#\0")
                {
                    Console.WriteLine($"{receive.RemoteEndPoint} connected!");
                    //Console.WriteLine($"{users.GetByIp(receive.RemoteEndPoint)} connected!");
                    //Send(
                    //    ConvertToJson(
                    //        this.chats.FindAll(
                    //            c => c.UsersList.Contains(
                    //                this.users.GetByIp(receive.RemoteEndPoint)))), receive.RemoteEndPoint);
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
        this.server.Send(data, address);
        try
        {
            if (!this.server.Send(data, address))
                throw new Exception("Unable to send");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Data has been succesfuly sent to {users.GetByIp(address)}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
    public void RegUsers()
    {
        //TODO: registrating clients
    }
    public string ConvertToJson<T>(T item) => "#info" + JsonSerializer.Serialize(item);
}

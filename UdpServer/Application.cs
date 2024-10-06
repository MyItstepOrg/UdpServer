using System.Net;
using UdpServer.Services.Services;
using System.Text;
using UdpServer.Core.Data.Dto;
using UdpServer.Services.Services.DataHandlers;

namespace UdpServer;

//TODO: Add services
public class Application(DataPacketsHandler packetsHandler)
{
    //Data
    private readonly DataPacketsHandler packetsHandler = packetsHandler;

    //Server
    public required Udp server;
    IPAddress ip = IPAddress.Parse("127.0.0.1");
    int port = 1025;

    public void Start()
    {
        //Initializing server
        try
        {
            server = new(new IPEndPoint(ip, port));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Server started...");

            //Begin receiving data
            Receive();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Target site: {ex.TargetSite} Exception: {ex.Message}");
        }
        finally
        {
            server.Close();
            Console.ForegroundColor = ConsoleColor.Cyan;
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
                DataPacket dataPacket = JsonProcessor.Deserialize(message);
                //Handling received data packet
                packetsHandler.HandleCommand(dataPacket, receive.RemoteEndPoint);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Target site: {ex.TargetSite} Exception: {ex.Message}");
            }
        }
    }
}

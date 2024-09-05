using System.Net;
using System.Net.Sockets;
using UdpServer.Services;

class Program
{
    public static async Task Main(string[] args)
    {
        //Create listener
        UdpService listener = new(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024));
        Console.WriteLine("Server started...");
        Console.WriteLine("Waiting for clients...");

        try
        {
            while (true)
            {
                var receive = await listener.Receive();
                string message = System.Text.Encoding.UTF8.GetString(receive.Buffer);
                Console.WriteLine($"{DateTime.Now} | {receive.RemoteEndPoint}: {message}");
                listener.Send(message);
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
        }
    }
}
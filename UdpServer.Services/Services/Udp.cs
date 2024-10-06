using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UdpServer.Services.Services;
public class Udp
{
    private readonly UdpClient socket;
    public Udp(IPEndPoint local) => socket = new UdpClient(local);

    public UdpReceiveResult Receive()
    {
        IPEndPoint remote = new(IPAddress.Any, 0);
        UdpReceiveResult receive = new(socket.Receive(ref remote), remote);
        return receive;
    }
    public bool Send(string datagramm, IPEndPoint endp)
    {
        try
        {
            byte[] data = Encoding.Unicode.GetBytes(datagramm);
            socket.Send(data, endp);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Data has been succesfuly sent to {endp}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to send data!");
            Console.WriteLine($"Target site: {ex.TargetSite} Exception: {ex.Message}");
            return false;
        }
        return true;
    }
    public void Close() => socket.Close();
}
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UdpServer.Core.Data.Source.Remote;
public class Udp
{
    private UdpClient socket;
    public Udp(IPEndPoint local)
    {
        socket = new UdpClient(local);
        socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
    }
    public async Task<UdpReceiveResult> Receive() => await socket.ReceiveAsync();
    public bool Send(string datagramm)
    {
        try
        {
            byte[] data = Encoding.Unicode.GetBytes(datagramm);
            socket.Send(data);
        }
        catch (SocketException ex)
        {
            Console.WriteLine("Socket exception: " + ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Send exception: " + ex.Message);
            return false;
        }
        return true;
    }
    public void Close() => socket.Close();
}
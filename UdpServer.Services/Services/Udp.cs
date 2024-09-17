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
        UdpReceiveResult receive = new(this.socket.Receive(ref remote), remote);
        return receive;
    }
    public bool Send(string datagramm, IPEndPoint endp)
    {
        try
        {
            byte[] data = Encoding.Unicode.GetBytes(datagramm);
            this.socket.Send(data, endp);
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
    public void Close() => this.socket.Close();
}
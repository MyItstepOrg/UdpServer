using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UdpServer.Core.Data.Source.Remote;
public class Udp
{
    private UdpClient socket;
    public Udp(IPEndPoint local) => socket = new UdpClient(local);
    public async Task<UdpReceiveResult> Receive() => await socket.ReceiveAsync();
    public bool Send(string datagramm, IPEndPoint endp)
    {
        try
        {
            byte[] data = Encoding.Unicode.GetBytes(datagramm);
            socket.Send(data, endp);
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
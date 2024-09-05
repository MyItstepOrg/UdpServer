using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UdpServer.Services;
public class UdpService
{
    private UdpClient socket;
    public UdpService(IPEndPoint local) => socket = new UdpClient(local);
    public async Task<UdpReceiveResult> Receive() => await this.socket.ReceiveAsync();
    public bool Send(string datagramm)
    {
        socket.EnableBroadcast = true;
        try
        {
            byte[] data = Encoding.Unicode.GetBytes(datagramm);
            this.socket.Send(data);
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
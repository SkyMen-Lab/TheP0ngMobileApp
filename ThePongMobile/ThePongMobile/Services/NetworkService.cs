using System.Net;
using System.Net.Sockets;
using System.Text;
using Xamarin.Forms.Internals;

namespace ThePongMobile.Services
{
    public class NetworkService : INetworkService
    {
        private int _score = 1;
        public void SendMessage(int direction)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SendTo(Encoding.ASCII.GetBytes(direction.ToString()), new IPEndPoint(IPAddress.Any, 3322));
            //Log.Warning("Network","Message sent");
        }

        public int ReceiveScore()
        {
           return _score++;
        }
    }
}
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThePongMobile.Models;
using Xamarin.Forms.Internals;

namespace ThePongMobile.Services
{
    public class NetworkServiceMock : INetworkService
    {
        private const string URL = "https://my-json-server.typicode.com/nnugget/TravelRecord/posts";
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

        public async Task<ConfigData> GetConfigDataAsync()
        {
            HttpClient _client = new HttpClient();
            string rawJson = await _client.GetStringAsync(URL);
            ConfigData config = JsonConvert.DeserializeObject<ConfigData>(rawJson);
            return config;
        }

        public int SendSchoolCode(string server, int port, string schoolCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
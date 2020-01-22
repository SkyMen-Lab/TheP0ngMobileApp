using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThePongMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ThePongMobile.Services.Mocks
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class NetworkServiceMock : INetworkService
    {
        private const string URL = "https://my-json-server.typicode.com/nnugget/TravelRecord/posts";
        private int _score = 1;
        public void SendMessage(int direction)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SendTo(Encoding.ASCII.GetBytes(direction.ToString()), new IPEndPoint(IPAddress.Any, 3322));
        }

        public Task<int> MakeHandshake(string server, int port, string schoolCode, string gameCode)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(server), port);
            //TODO: exception handling
            socket.Connect(endPoint);
            byte[] schoolCodeBytes = Encoding.ASCII.GetBytes(schoolCode);
            socket.Send(schoolCodeBytes);
            byte[] tcpResponse = new byte[1];
            socket.Receive(tcpResponse);
            socket.Close();
            return Task.FromResult<int>(tcpResponse[0]);
        }

        public int ReceiveScore()
        {
           return _score++;
        }

        public async Task<SchoolData> GetSchoolDataAsync(string code)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //TODO: replace with domain
            string newUrl;
            if (Device.RuntimePlatform == Device.Android) 
                 newUrl = "http://10.0.2.2:5000/api/team/code/" + code;
            else newUrl = "http://localhost:5000/api/team/code/" + code;
            string rawJson = await client.GetStringAsync(newUrl);
            SchoolData school = JsonConvert.DeserializeObject<SchoolData>(rawJson);
            return school;
        }
    }
}
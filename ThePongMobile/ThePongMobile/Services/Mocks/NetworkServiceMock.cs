using System;
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
        byte[] directionBytes = new byte[1];
        private UdpClient udpClient;
        private IPEndPoint endPoint;


        public void SendMessage(int direction)
        {
            directionBytes[0] = (byte)direction;
            try
            {
                udpClient.Send(directionBytes, 1, endPoint);
            }
            catch
            {

            }
        }

        public Task<int> MakeHandshake(string server, int port, string schoolCode, string gameCode)
        {
            var sendingModel = new HandShakeJsonModel()
            {
                GameCode = gameCode,
                SchoolCode = schoolCode
            };
            string JsonToSend = JsonConvert.SerializeObject(sendingModel);
            byte[] tcpResponse = new byte[32];
            byte[] schoolCodeBytes = Encoding.ASCII.GetBytes(JsonToSend);
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                udpClient = new UdpClient();
                endPoint = new IPEndPoint(IPAddress.Any, port);

                socket.Connect(endPoint);
                socket.Send(schoolCodeBytes);
                socket.Receive(tcpResponse);
                socket.Close();
            }
            catch (Exception e)
            {
                return Task.FromResult(404);
            }

            if (int.TryParse(Encoding.ASCII.GetString(tcpResponse), out int httpResponse))
            {
                return Task.FromResult<int>(httpResponse);
            }

            return Task.FromResult(404);
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
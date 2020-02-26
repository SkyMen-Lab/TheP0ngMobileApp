using System;
using System.ComponentModel.Design;
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
        private static UdpClient _udpClient;
        private static IPEndPoint _endPoint;


        public void SendMessage(int direction)
        {
            directionBytes[0] = (byte)direction;
            try
            {
                _udpClient.Send(directionBytes, 1, _endPoint);
            }
            catch
            {

            }
        }

        public Task<int> MakeHandshake(string server, int port, string schoolCode, string gameCode, bool isJoining)
        {
            var sendingModel = new HandShakeJsonModel()
            {
                GameCode = gameCode,
                SchoolCode = schoolCode,
                IsJoining = isJoining
            };
            string jsonToSend =  JsonConvert.SerializeObject(sendingModel);
            byte[] tcpResponse = new byte[64];
            byte[] schoolCodeBytes = Encoding.ASCII.GetBytes(jsonToSend);

            server = "10.0.2.2";

            try
            {
                _udpClient = new UdpClient();
                _endPoint = new IPEndPoint(IPAddress.Parse(server), port);

                TcpClient client = new TcpClient(server, port + 1);
                NetworkStream ns = client.GetStream();
                ns.Write(schoolCodeBytes, 0, schoolCodeBytes.Length);
                ns.Read(tcpResponse, 0, tcpResponse.Length);
                ns.Close();
                client.Close();
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
                 newUrl = "http://10.0.2.2:5007/api/team/code/" + code;
            else newUrl = "http://localhost:5007/api/team/code/" + code;
            string rawJson = await client.GetStringAsync(newUrl);
            SchoolData school = JsonConvert.DeserializeObject<SchoolData>(rawJson);
            return school;
        }
    }
}
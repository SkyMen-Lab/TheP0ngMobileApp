using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThePongMobile.Models;

namespace ThePongMobile.Services
{
    public class NetworkService : INetworkService
    {
        private const string URL = "https://my-json-server.typicode.com/nnugget/TravelRecord/posts";

        public async Task<ConfigData> GetConfigDataAsync()
        {
            HttpClient _client = new HttpClient();
            string rawJson = await _client.GetStringAsync(URL);
            ConfigData config = JsonConvert.DeserializeObject<ConfigData>(rawJson);
            return config;
        }

        public int ReceiveScore()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(int direction)
        {
            throw new NotImplementedException();
        }

        public int SendSchoolCode(string server, int port, string schoolCode)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(server), port);
            socket.Connect(endPoint);
            byte[] schoolCodeBytes = Encoding.ASCII.GetBytes(schoolCode);
            socket.Send(schoolCodeBytes);
            byte[] tcpResponse = new byte[1];
            socket.Receive(tcpResponse);
            socket.Close();
            return tcpResponse[0];
        }
    }
}

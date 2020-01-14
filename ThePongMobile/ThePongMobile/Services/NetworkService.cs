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
            TcpClient client = new TcpClient(server, port);
            byte[] schoolCodeBytes = Encoding.ASCII.GetBytes(schoolCode);
            NetworkStream stream = client.GetStream();
            stream.Write(schoolCodeBytes, 0, schoolCodeBytes.Length);
            byte[] tcpResponse = new byte[1];
            stream.Read(tcpResponse, 0, 1);
            //Sends 1 if successful handshake and 0 if unsuccessful;
            //Don't know if a certain message as a response would be better?
            int response = tcpResponse[0];
            stream.Close();
            client.Close();
            return response;
        }
    }
}

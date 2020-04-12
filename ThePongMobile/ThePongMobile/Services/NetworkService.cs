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
        //TODO: remove use NetworkServiceMock
        private const string URL = "https://my-json-server.typicode.com/nnugget/TravelRecord/posts";

        public int ReceiveScore()
        {
            throw new NotImplementedException();
        }

        public async Task<SchoolData> GetSchoolDataAsync(string code)
        {
            HttpClient _client = new HttpClient();
            //TODO: replace with the domain API
            string rawJson = await _client.GetStringAsync(URL);
            SchoolData config = JsonConvert.DeserializeObject<SchoolData>(rawJson);
            //TODO: error checking return null if the code doesn't match any team
            return config;
        }

        public void SendMessage(int direction)
        {
            throw new NotImplementedException();
        }
        public Task<int> UserLeaving()
        {
            throw new NotImplementedException();
        }

        public Task<int> MakeHandshake(string server, int port, string schoolCode, string gameCode, bool userJoined)
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
        public Task<int> LeaveGame(string server, int port, string schoolCode, string gameCode, bool isJoining, ConnectionType connectionType)
        {
            throw new NotImplementedException();
        }

        public Task<int> JoinGame(string server, int port, string schoolCode, string gameCode, bool isJoining, ConnectionType connectionType)
        {
            throw new NotImplementedException();
        }
    }
}

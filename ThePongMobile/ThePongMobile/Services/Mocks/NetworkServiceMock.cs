using System;
using System.ComponentModel.Design;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThePongMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ThePongMobile.Services.Mocks
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class NetworkServiceMock : INetworkService
    {
        private int _score = 1;
        byte[] directionBytes = new byte[1];
        ConnectionType _connectionType;
        private TcpClient gameClient;
        private NetworkStream gameStream;
        private static UdpClient _udpClient;
        private static IPEndPoint _endPoint;

        public void SendMessage(int direction)
        {
            if(_connectionType == ConnectionType.UDP)
            {
                SendMessageUDP(direction);
            }
            else if(_connectionType == ConnectionType.TCP)
            {
                SendMessageTCP(direction);
            }
        }

        public Task<int> LeaveGame(string server, int port, string schoolCode, string gameCode, bool isJoining, ConnectionType connectionType) 
            => MakeHandshake(server, port, schoolCode, gameCode, isJoining, connectionType);

         public Task<int> JoinGame(string server, int port, string schoolCode, string gameCode, bool isJoining , ConnectionType connectionType) 
            => MakeHandshake(server, port, schoolCode, gameCode, isJoining, connectionType);

        public int ReceiveScore()
        {
           return _score++;
        }

        public async Task<SchoolData> GetSchoolDataAsync(string code)
        {
            string Acode = code.ToUpper();
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //TODO: replace with domain
            string newUrl;
            newUrl = "https://api.thep0ng.net/storage/api/team/code/" + Acode;
            SchoolData schooldata = null;
            try
            {
                string rawJson = await client.GetStringAsync(newUrl);
                schooldata = JsonConvert.DeserializeObject<SchoolData>(rawJson);
            }catch{ }
            return schooldata;
        }
        private Task<int> MakeHandshake(string server, int port, string schoolCode, string gameCode, bool isJoining, ConnectionType connectionType)
        {
            _connectionType = connectionType;
            var sendingModel = new HandShakeJsonModel()
            {
                GameCode = gameCode,
                TeamCode = schoolCode.ToUpper(),
                IsJoining = isJoining
            };
            string jsonToSend =  JsonConvert.SerializeObject(sendingModel);
            byte[] tcpResponse = new byte[64];
            byte[] schoolCodeBytes = Encoding.ASCII.GetBytes(jsonToSend);

            server = "178.62.110.205";

            try
            {
                TcpClient handshakeClient = new TcpClient(server, port + 1);
                NetworkStream ns = handshakeClient.GetStream();
                ns.Write(schoolCodeBytes, 0, schoolCodeBytes.Length);
                ns.Read(tcpResponse, 0, tcpResponse.Length);
                ns.Close();
                handshakeClient.Close();
            }
            catch
            {
                return Task.FromResult(404);
            }
            if (int.TryParse(Encoding.ASCII.GetString(tcpResponse), out int httpResponse))
            {
                if (!isJoining)
                {
                    return Task.FromResult<int>(httpResponse);
                }
                else if(httpResponse == 200 && connectionType == ConnectionType.TCP)
                {
                    //Start TCP connection.
                    ConnectToGameTCP(server, port);
                }
                else if(httpResponse == 200 && connectionType == ConnectionType.UDP)
                {
                    //Start UDP connection.
                    FindGameUDP(server, port);
                }
                return Task.FromResult<int>(httpResponse);
            }
            return Task.FromResult(404);
        }
        private void DisconnectFromGameTCP()
        {
            gameStream.FlushAsync();
            gameStream.Close();
            gameClient.Dispose();
            gameClient.Close();
        }
        private async void ConnectToGameTCP(string server, int port)
        {
            try
            {
                gameClient = new TcpClient();
                await gameClient.ConnectAsync(server, port);
                gameStream = gameClient.GetStream(); 
            }
            catch
            {
                DisconnectFromGameTCP();
            }
        }
        private void FindGameUDP(string server, int port)
        {
            _udpClient = new UdpClient();
            _endPoint = new IPEndPoint(IPAddress.Parse(server), port);
            _udpClient.Connect(_endPoint);
        }
        private void SendMessageUDP(int direction)
        {
            directionBytes[0] = (byte)direction;
            try
            {
                _udpClient.Send(directionBytes, 1);
            }
            catch (Exception e)
            {
                Log.Warning("error", e.Message);
            }
        }
        private void SendMessageTCP(int direction)
        {
            directionBytes[0] = (byte)direction;
            try
            {
                gameStream.Write(directionBytes, 0, 1);
            }
            catch(IOException)
            {
                DisconnectFromGameTCP();
            }
            catch
            {

            }
        }
    }
}
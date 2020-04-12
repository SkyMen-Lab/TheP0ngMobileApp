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

namespace ThePongMobile.Services.Mocks
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class NetworkServiceMock : INetworkService
    {
        private int _score = 1;
        byte[] directionBytes = new byte[1];
        TcpClient gameClient;
        NetworkStream gameStream;

        public void SendMessage(int direction)
        {
            directionBytes[0] = (byte)direction;
            try
            {
               gameStream.Write(directionBytes, 0, 1);
            }
            catch(IOException)
            {
                DisconnectFromGame();
            }
            catch
            {

            }
        }
        private void DisconnectFromGame()
        {
            gameStream.FlushAsync();
            gameStream.Close();
            gameClient.Dispose();
            gameClient.Close();
        }
        private async void ConnectToGame(string server, int port)
        {
            try
            {
                gameClient = new TcpClient();
                await gameClient.ConnectAsync(server, port);
                gameStream = gameClient.GetStream(); 
            }
            catch
            {
                DisconnectFromGame();
            }
        }



        public Task<int> LeaveGame(string server, int port, string schoolCode, string gameCode, bool isJoining) 
            => MakeHandshake(server, port, schoolCode, gameCode, isJoining);

         public Task<int> JoinGame(string server, int port, string schoolCode, string gameCode, bool isJoining) 
            => MakeHandshake(server, port, schoolCode, gameCode, isJoining);

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
            newUrl = "https://api-storage.thep0ng.io/" + Acode;
            SchoolData schooldata = null;
            try
            {
                string rawJson = await client.GetStringAsync(newUrl);
                schooldata = JsonConvert.DeserializeObject<SchoolData>(rawJson);
            }catch{ }
            return schooldata;
        }
        private Task<int> MakeHandshake(string server, int port, string schoolCode, string gameCode, bool isJoining)
        {
            var sendingModel = new HandShakeJsonModel()
            {
                GameCode = gameCode,
                TeamCode = schoolCode.ToUpper(),
                IsJoining = isJoining
            };
            string jsonToSend =  JsonConvert.SerializeObject(sendingModel);
            byte[] tcpResponse = new byte[64];
            byte[] schoolCodeBytes = Encoding.ASCII.GetBytes(jsonToSend);

            server = "127.0.0.1";

            try
            {
                TcpClient handshakeClient = new TcpClient(server, port + 1);
                NetworkStream ns = handshakeClient.GetStream();
                ns.Write(schoolCodeBytes, 0, schoolCodeBytes.Length);
                ns.Read(tcpResponse, 0, tcpResponse.Length);
                ns.Close();
                handshakeClient.Close();
            }
            catch (Exception e)
            {
                return Task.FromResult(404);
            }
            if (int.TryParse(Encoding.ASCII.GetString(tcpResponse), out int httpResponse))
            {
                if(httpResponse == 200)
                {
                    //With a correct User Joined event from router, it will start an
                    //async connection to the router filter.
                    ConnectToGame(server, port);
                }
                return Task.FromResult<int>(httpResponse);
            }
            return Task.FromResult(404);
        }
    }
}
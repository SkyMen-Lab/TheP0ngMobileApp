using System;
using System.Collections.Concurrent;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        private static UdpClient _udpClient;
        private static IPEndPoint _endPoint;
        private ConcurrentQueue<int> _directionQueue;
        private Thread _readThread;


        public void SendMessage(int direction)
        {
            _directionQueue.Enqueue(direction);
        }
        
        private void ReadQueueAndSendMessage()
        {
            while (true)
            {
                int direction;
                if (_directionQueue.TryDequeue(out direction))
                {
                    directionBytes[0] = (byte)direction;
                    try
                    {
                        _udpClient.Send(directionBytes, 1, _endPoint);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        public async Task<int> LeaveGame(string server, int port, string schoolCode, string gameCode, bool isJoining)
        {
            _readThread.Abort();
            _readThread = null;
            return await MakeHandshake(server, port, schoolCode, gameCode, isJoining);
        }

        public async Task<int> JoinGame(string server, int port, string schoolCode, string gameCode, bool isJoining)
        {
            _directionQueue = new ConcurrentQueue<int>();
            _readThread = new Thread(ReadQueueAndSendMessage);
            _readThread.IsBackground = true;
            _readThread.Start();
            return await MakeHandshake(server, port, schoolCode, gameCode, isJoining);
        }

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
            newUrl = "http://10.4.191.231/api/team/code/" + Acode;
            SchoolData schooldata = null;
            try
            {
                string rawJson = await client.GetStringAsync(newUrl);
                schooldata = JsonConvert.DeserializeObject<SchoolData>(rawJson);
            }catch (Exception e){
                Console.WriteLine(e.Message);
            }
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

            //server = "10.4.191.231";

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
            catch
            {
                return Task.FromResult(404);
            }
            if (int.TryParse(Encoding.ASCII.GetString(tcpResponse), out int httpResponse))
            {
                return Task.FromResult<int>(httpResponse);
            }
            return Task.FromResult(404);
        }
    }
}
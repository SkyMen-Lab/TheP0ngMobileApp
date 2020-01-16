using System.Threading.Tasks;
using ThePongMobile.Models;

namespace ThePongMobile.Services
{
    public interface INetworkService
    {
        void SendMessage(int direction);
        int MakeHandshake(string server, int port, string schoolCode);
        int ReceiveScore();
        Task<ConfigData> GetConfigDataAsync();

    }
}
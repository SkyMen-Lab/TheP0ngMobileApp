using System.Threading.Tasks;
using ThePongMobile.Models;

namespace ThePongMobile.Services
{
    public interface INetworkService
    {
        void SendMessage(int direction);
<<<<<<< HEAD
=======
        int SendSchoolCode(string server, int port, string schoolCode);
>>>>>>> Added TCP client
        int ReceiveScore();
        Task<ConfigData> GetConfigDataAsync();

    }
}
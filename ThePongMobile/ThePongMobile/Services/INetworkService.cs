using System.Threading.Tasks;
using ThePongMobile.Models;

namespace ThePongMobile.Services
{
    public interface INetworkService
    {
        void SendMessage(int direction);
        Task<int> MakeHandshake(string server, int port, string schoolCode, string gameCode);
        int ReceiveScore();
        Task<SchoolData> GetSchoolDataAsync(string code);

    }
}
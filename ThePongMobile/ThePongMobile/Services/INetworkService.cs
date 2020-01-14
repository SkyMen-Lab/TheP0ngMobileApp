using System.Threading.Tasks;
using ThePongMobile.Models;

namespace ThePongMobile.Services
{
    public interface INetworkService
    {
        void SendMessage(int direction);
        int ReceiveScore();
        Task<SchoolData> GetSchoolDataAsync(string code);

    }
}
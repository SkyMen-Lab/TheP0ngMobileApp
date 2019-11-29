using ThePongMobile.Models;

namespace ThePongMobile.Services
{
    public interface IStorageService
    {
        void GetConfiguration();
        void SetConfiguration(ConnectionConfig config);
    }
}
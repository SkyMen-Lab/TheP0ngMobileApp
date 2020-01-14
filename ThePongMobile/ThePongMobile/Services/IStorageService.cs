using ThePongMobile.Models;

namespace ThePongMobile.Services
{
    public interface IStorageService<T> where T : class
    {
        T GetConfiguration();
        void SetConfiguration(T config);
        void ClearConfiguration();
    }
}
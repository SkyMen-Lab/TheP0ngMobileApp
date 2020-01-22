using Xamarin.Essentials;
using ThePongMobile.Models;

namespace ThePongMobile.Services
{
    public class StorageService : IStorageService<SettingsModel>
    {
        private const string SchoolNameDefault = "N/A";
        private const string SchoolCodeDefault = "N/A";
        private const string IpDefault = "N/A";
        private const int PortDefault = -1;
        private const int ConnectionTypeDefault = -1;
        public SettingsModel GetConfiguration()
        {
            string ip = Preferences.Get("ip", IpDefault);
            if (string.Equals(ip, IpDefault)) return null;
            int port = Preferences.Get("port", PortDefault);
            if (port == PortDefault) return null;
            string schoolCode = Preferences.Get("name", SchoolCodeDefault);
            if (schoolCode == SchoolCodeDefault) return null;
            string schoolName = Preferences.Get("code", SchoolNameDefault);
            if (schoolName == SchoolNameDefault) return null;
            int connectionType = Preferences.Get("c_type", ConnectionTypeDefault);
            if (connectionType == -1) return null;
            
            
            var config = new SettingsModel()
            {
                SchoolName = schoolName, 
                SchoolCode = schoolCode,
                IP = ip,
                Port = port,
                ConnectionType = (ConnectionType) connectionType
            };
            return config;
        }

        public void SetConfiguration(SettingsModel config)
        { 
            ClearConfiguration();
            Preferences.Set("code", config.SchoolCode);
            Preferences.Set("name", config.SchoolName);
            Preferences.Set("ip", config.IP);
            Preferences.Set("port", config.Port);
            Preferences.Set("c_type", (int)config.ConnectionType);
            Preferences.Set("setup", true);
        }

        public bool IsSetup()
        {
            return Preferences.ContainsKey("setup");
        }
        

        public void ClearConfiguration()
        {
            Preferences.Clear();
        }
    }
}
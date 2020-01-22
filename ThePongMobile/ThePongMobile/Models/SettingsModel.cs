namespace ThePongMobile.Models
{
    public class SettingsModel
    {
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public ConnectionType ConnectionType { get; set; }
    }
}
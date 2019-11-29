namespace ThePongMobile.Models
{
    public class ConnectionConfig
    {
        private int _port;

        public int Port
        {
            get => _port;
            set => _port = value;
        }

        private string _ip;

        public string Ip
        {
            get => _ip;
            set => _ip = value;
        }
    }
}
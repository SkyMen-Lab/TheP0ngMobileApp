using Newtonsoft.Json;

namespace ThePongMobile.Models
{
    public class ConnectionConfig
    {
        [JsonProperty("routerIpAddress")]
        public string IP { get; set; }
        [JsonProperty("routerPort")]
        public int Port { get; set; }
        [JsonProperty("connectionType")]
        public ConnectionType ConnectionType { get; set; }
    }

    public enum ConnectionType
    {
        UDP,
        TCP
    }
}
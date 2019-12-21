using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThePongMobile.Models
{
    public partial class ConfigData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("winningRate")]
        public long WinningRate { get; set; }

        [JsonProperty("configId")]
        public long ConfigId { get; set; }

        [JsonProperty("config")]
        public Config Config { get; set; }

        [JsonProperty("gamesWon")]
        public object GamesWon { get; set; }

        [JsonProperty("teamGameSummaries")]
        public object TeamGameSummaries { get; set; }
    }

    public partial class Config
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("routerIpAddress")]
        public string RouterIpAddress { get; set; }

        [JsonProperty("routerPort")]
        public long RouterPort { get; set; }

        [JsonProperty("connectionType")]
        public long ConnectionType { get; set; }
    }
}

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
    }
}

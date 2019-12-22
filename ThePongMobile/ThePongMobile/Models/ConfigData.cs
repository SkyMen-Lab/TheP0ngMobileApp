using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThePongMobile.Models
{
    public partial class ConfigData
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("winningRate")]
        public int WinningRate { get; set; }

        [JsonProperty("configId")]
        public int ConfigId { get; set; }
    }
}


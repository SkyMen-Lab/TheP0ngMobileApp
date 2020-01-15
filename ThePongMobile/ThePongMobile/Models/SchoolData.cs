using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThePongMobile.Models
{
    public class SchoolData
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
        public double WinningRate { get; set; }

        [JsonProperty("configId")]
        public int ConfigId { get; set; }
        
        [JsonProperty("config")]
        public ConnectionConfig Config { get; set; }
    }
}


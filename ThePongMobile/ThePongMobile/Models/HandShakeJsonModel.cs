using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ThePongMobile.Models
{
    public class HandShakeJsonModel
    {
        [JsonProperty("TeamCode")]
        public string TeamCode { get; set; }

        [JsonProperty("GameCode")]
        public string GameCode { get; set; }

        [JsonProperty("IsJoining")]
        public bool IsJoining { get; set; }

       
    }
}

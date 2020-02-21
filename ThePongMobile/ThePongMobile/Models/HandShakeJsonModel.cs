using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ThePongMobile.Models
{
    public class HandShakeJsonModel
    {
        [JsonProperty("SchoolCode")]
        public string SchoolCode { get; set; }

        [JsonProperty("GameCode")]
        public string GameCode { get; set; }

       
    }
}

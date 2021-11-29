using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WLBApplication.Model
{
    public class InputWeight
    {
        public string RequestedWeight { get; set; }
        [JsonIgnore]
        public decimal weight { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string error { get; set; }
        [JsonIgnore]
        public bool isValid { get; set; } = true;
    }
}

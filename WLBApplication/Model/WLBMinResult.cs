using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WLBApplication.Model
{
    [Serializable]
    public class WLBMinResult
    {
        public string  RequestedWeight{ get; set; }
        public Dictionary<string, int> minWeightList = new Dictionary<string, int>();
        [JsonIgnore]
        public int platesCount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string error { get; set; }
    }
}

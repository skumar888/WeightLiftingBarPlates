using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WLBApplication.Model
{
    [Serializable]
    public class WLBMinResult
    {
        public string  requestedWeight{ get; set; }
        public Dictionary<string, int> minWeightList = new Dictionary<string, int>();
        [JsonIgnore]
        public int platesCount { get; set; }
        public string error { get; set; }
    }
}

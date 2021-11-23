using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WLPBlatesManager.Model
{
    public class Plate
    {
        public string Name { get; set; }
        public decimal weight { get; set; }

        [JsonIgnore]
        public decimal pairedWeight { get { return weight * 2; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WLPBlatesManager.Model
{
    public class Plate
    {
        public string Name { get; set; }
        public double weight { get; set; }
        public double pairedWeight { get { return weight * 2; } }
    }
}

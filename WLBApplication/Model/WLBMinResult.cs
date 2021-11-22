using System;
using System.Collections.Generic;
using System.Text;

namespace WLBApplication.Model
{
    public class WLBMinResult
    {
        public double  requestedWeight{ get; set; }
        public List<KeyValuePair<string, int>> minWeightList = new List<KeyValuePair<string, int>>();
    }
}

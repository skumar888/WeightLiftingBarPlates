using System;
using System.Collections.Generic;
using System.Text;

namespace WLBApplication.Model
{
    public class WLBMinResult
    {
        public int  requestedWeight{ get; set; }
        public Dictionary<string, int> minWeightList{ get; set; }
    }
}

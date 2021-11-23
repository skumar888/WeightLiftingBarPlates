using System;
using System.Collections.Generic;
using System.Text;

namespace WLBApplication.Model
{
    public class InputWeight
    {
        public string weightName { get; set; }
        public decimal weight { get; set; }
        public string error { get; set; }
        public bool isValid { get; set; } = true;
    }
}

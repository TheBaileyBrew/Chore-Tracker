using System;
using System.Collections.Generic;
using System.Text;

namespace ChoreTracker.Models
{
    class Tasks
    {
        public string TaskTitle { get; set; }
        public string TaskShortDescription { get; set; }
        public string TaskLongDescription { get; set; }
        public double TaskWorthSwitchTime { get; set; }
        public double TaskWorthDollarValue { get; set; }
    }
}

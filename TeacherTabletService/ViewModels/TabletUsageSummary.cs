using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherTabletService.ViewModels
{   

    public class TabletUsageSummary
    {        
        public string serialNumber { get; set; }
        public decimal hoursUsed { get; set; }
        public decimal percentageBatteryUsage { get; set; }
        public decimal dailyAverageBatteryUsage { get; set; }
        
        public string batteryStatus 
        {
            get
            {
                if(hoursUsed == 0)
                {
                    return nameof(BatteryStatus.UNKNOWN);
                }
                else if(dailyAverageBatteryUsage > 30) 
                {
                    return nameof(BatteryStatus.NEEDS_REPLACEMENT);
                }
                else
                {
                    return nameof(BatteryStatus.OK);
                }
            }
        }
    }
}

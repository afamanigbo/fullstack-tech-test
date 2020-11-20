using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherTabletService.Models
{
    public class TabletUsageData
    {
        public int academyId { get; set; }
        public decimal batteryLevel { get; set; }
        public string employeeId { get; set; }
        public string serialNumber { get; set; }
        public DateTime timestamp { get; set; }
    }

}

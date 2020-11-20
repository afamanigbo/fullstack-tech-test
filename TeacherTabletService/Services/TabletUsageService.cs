using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherTabletService.Interfaces;
using TeacherTabletService.Models;
using TeacherTabletService.ViewModels;

namespace TeacherTabletService.Services
{
    

    public class TabletUsageService : ITabletUsageService
    {

        public List<TabletUsageData> SortTablets(List<TabletUsageData> data)
        {
            return (from c in data
                    orderby c.serialNumber, c.timestamp
                    select c).ToList();
        }

        public List<TabletUsageSummary> GetTabletUsageSummary(List<TabletUsageData> data)
        {
            List<TabletUsageSummary> tabletUsageSummary = new List<TabletUsageSummary>();

            string currentSerialNumber = string.Empty;
            decimal currentBatteryPercentage = 0;
            DateTime currentDateTime = DateTime.MinValue;

            decimal minutesTotal = 0;
            decimal percentageTotal = 0;

            int count = 0;

            foreach (var tabletData in data)
            {
                count++;

                if (string.IsNullOrEmpty(currentSerialNumber))
                {
                    currentSerialNumber = tabletData.serialNumber;
                    currentBatteryPercentage = tabletData.batteryLevel;
                    currentDateTime = tabletData.timestamp;
                    continue;
                }

                if (tabletData.serialNumber == currentSerialNumber)
                {
                    if (tabletData.batteryLevel <= currentBatteryPercentage)
                    {
                        percentageTotal += currentBatteryPercentage - tabletData.batteryLevel;
                        minutesTotal += (decimal) tabletData.timestamp.Subtract(currentDateTime).TotalMinutes;
                    }
                    
                }
                else
                {
                    tabletUsageSummary.Add(new TabletUsageSummary
                    {
                        serialNumber = currentSerialNumber,
                        hoursUsed = minutesTotal > 0 ? Decimal.Round(minutesTotal / 60 , 2) : 0,
                        percentageBatteryUsage = percentageTotal * 100,
                        dailyAverageBatteryUsage = percentageTotal > 0 ?
                        Decimal.Round((percentageTotal * 24 / (minutesTotal/60)) * 100, 2) : 0
                    });

                }

                currentSerialNumber = tabletData.serialNumber;
                currentBatteryPercentage = tabletData.batteryLevel;
                currentDateTime = tabletData.timestamp;

                if (count == data.Count)
                {
                    tabletUsageSummary.Add(new TabletUsageSummary
                    {
                        serialNumber = currentSerialNumber,
                        hoursUsed = minutesTotal > 0 ? Decimal.Round(minutesTotal / 60, 2) : 0,
                        percentageBatteryUsage = percentageTotal * 100,
                        dailyAverageBatteryUsage = percentageTotal > 0 ?
                        Decimal.Round((percentageTotal * 24 / (minutesTotal / 60)) * 100, 2) : 0
                    });
                }

                
            }

            if ((!string.IsNullOrEmpty(currentSerialNumber)) && (data.Count == 1))
            {
                tabletUsageSummary.Add(new TabletUsageSummary
                {
                    serialNumber = currentSerialNumber,
                    hoursUsed = 0,
                    percentageBatteryUsage = 0,
                    dailyAverageBatteryUsage = 0
                });
            }

            return tabletUsageSummary;
        }

        
    }
}

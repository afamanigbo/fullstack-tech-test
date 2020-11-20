using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherTabletService.Models;
using TeacherTabletService.Repository;
using TeacherTabletService.Services;
using TeacherTabletService.ViewModels;

namespace TestTeacherTabletService
{
    public class TestService
    {
        List<TabletUsageData> sortedData = new List<TabletUsageData>();

       
        private void LoadData(string filename)
        {
            DataLoader loader =
                new DataLoader
                (new NullLogger<DataLoader>());

            List<TabletUsageData> tabletUsageData =
                new List<TabletUsageData>();

            tabletUsageData = loader.LoadDataFromFile(filename);

            TabletUsageService service = new TabletUsageService();

            sortedData = service.SortTablets(tabletUsageData);
        }

        [Test]
        public void TestDataSorting()
        {
            LoadData("dataTestService1.json");

            Assert.AreEqual(sortedData[0].timestamp, DateTime.Parse("2019-05-17T09:00:00.000+01:00"));
            Assert.AreEqual(sortedData[1].timestamp, DateTime.Parse("2019-05-17T21:00:00.000+01:00"));
            Assert.AreEqual(sortedData[2].timestamp, DateTime.Parse("2019-05-18T21:00:00.000+01:00"));
        }

        [Test]
        public void TestSummaryOneTabletMultipleTimestamps()
        {
            LoadData("dataTestService1.json");

            List<TabletUsageSummary> summaryOutput = new List<TabletUsageSummary>();
            TabletUsageService service = new TabletUsageService();

            summaryOutput = service.GetTabletUsageSummary(sortedData);

            Assert.IsNotNull(summaryOutput);
            Assert.IsTrue(summaryOutput.Count > 0);
            Assert.AreEqual(summaryOutput[0].hoursUsed, 36);
            Assert.AreEqual(summaryOutput[0].percentageBatteryUsage, 20);
            Assert.AreEqual(summaryOutput[0].dailyAverageBatteryUsage, 13.33);
            Assert.AreEqual(summaryOutput[0].batteryStatus, "OK");
    
        }

        [Test]
        public void TestSummaryOneTabletSingeTimestamp()
        {
            LoadData("dataTestService2.json");

            List<TabletUsageSummary> summaryOutput = new List<TabletUsageSummary>();
            TabletUsageService service = new TabletUsageService();

            summaryOutput = service.GetTabletUsageSummary(sortedData);

            Assert.IsNotNull(summaryOutput);
            Assert.IsTrue(summaryOutput.Count > 0);
            Assert.AreEqual(summaryOutput[0].hoursUsed, 0);
            Assert.AreEqual(summaryOutput[0].percentageBatteryUsage, 0);
            Assert.AreEqual(summaryOutput[0].dailyAverageBatteryUsage, 0);
            Assert.AreEqual(summaryOutput[0].batteryStatus, "UNKNOWN");

        }
    }
}

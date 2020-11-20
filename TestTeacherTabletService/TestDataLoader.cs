using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestTeacherTabletService
{
    public class TestDataLoader
    {

        [Test]
        public void TestLoadData()
        {
            TeacherTabletService.Repository.DataLoader loader =
                new TeacherTabletService.Repository.DataLoader
                (new NullLogger<TeacherTabletService.Repository.DataLoader>());

            List<TeacherTabletService.Models.TabletUsageData> tabletUsageData =
                new List<TeacherTabletService.Models.TabletUsageData>();

            tabletUsageData = loader.LoadDataFromFile("dataTestDataLoader.json");

            Assert.IsNotNull(tabletUsageData);
            Assert.IsTrue(tabletUsageData.Count > 0);

            Assert.AreEqual(tabletUsageData[0].academyId, 30021);
            Assert.AreEqual(tabletUsageData[0].batteryLevel, 1.0);
            Assert.AreEqual(tabletUsageData[0].employeeId, "T1001452");
            Assert.AreEqual(tabletUsageData[0].serialNumber, "1805C67HD01951");
            Assert.AreEqual(tabletUsageData[0].timestamp, DateTime.Parse("2019-05-17T09:00:00.000+01:00"));
        }

        [Test]
        public void TestLoadDataWrongFile()
        {
            TeacherTabletService.Repository.DataLoader loader =
                new TeacherTabletService.Repository.DataLoader
                (new NullLogger<TeacherTabletService.Repository.DataLoader>());

            List<TeacherTabletService.Models.TabletUsageData> tabletUsageData =
                new List<TeacherTabletService.Models.TabletUsageData>();

            tabletUsageData = loader.LoadDataFromFile("dataTestLoaderWrongFile.json");

            Assert.IsNotNull(tabletUsageData);
            Assert.IsTrue(tabletUsageData.Count == 0);
        }
    }
}
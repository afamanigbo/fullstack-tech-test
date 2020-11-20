using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TeacherTabletService.Models;
using Microsoft.Extensions.Logging;
using TeacherTabletService.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace TeacherTabletService.Repository
{
    
    public class DataLoader : IDataLoader
    {
        private readonly ILogger _logger;

        public DataLoader(ILogger<DataLoader> logger)
        {
            _logger = logger;            
        }

        public List<TabletUsageData> LoadDataFromFile(string fileName)
        {
            List<TabletUsageData> tabletBatteryUsageData = new List<TabletUsageData>();

            try
            {
                string jsonString = File.ReadAllText(fileName);
                tabletBatteryUsageData = JsonSerializer.Deserialize<List<TabletUsageData>>(jsonString);
                
                return tabletBatteryUsageData;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading data: {ex.Message}");
                return tabletBatteryUsageData;
            }
        }
        
    }
}

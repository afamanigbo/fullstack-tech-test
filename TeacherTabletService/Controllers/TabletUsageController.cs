using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TeacherTabletService.Interfaces;
using TeacherTabletService.Models;
using TeacherTabletService.ViewModels;

namespace TeacherTabletService.Controllers
{
    [Route("api/tabletusage")]
    [ApiController]
    public class TabletUsageController : ControllerBase
    {
        private readonly IDataLoader _dataLoader;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly ITabletUsageService _tabletUsageService;
        private readonly ILogger _logger;


        public TabletUsageController(IConfiguration configuration, IDataLoader dataLoader, 
            IWebHostEnvironment webHostEnvironment, ITabletUsageService tabletUsageService,
            ILogger<TabletUsageController> logger)
        {
            _configuration = configuration;
            _dataLoader = dataLoader;
            _webHostingEnvironment = webHostEnvironment;
            _tabletUsageService = tabletUsageService;
            _logger = logger;
        }
                

        private List<TabletUsageData> LoadData()
        {
            try
            {
                List<TabletUsageData> tabletUsageData = new List<TabletUsageData>();

                string fileName = _webHostingEnvironment.ContentRootPath + "\\" + _configuration["DataLocation"];
                tabletUsageData = _dataLoader.LoadDataFromFile(fileName);

                return tabletUsageData;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<TabletUsageController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<TabletUsageData> tabletUsageData = new List<TabletUsageData>();
                tabletUsageData = LoadData();
                                                
                if(tabletUsageData.Count == 0)
                {
                    return NotFound("Could not load data");
                }

                List<TabletUsageData> sortedTabletUsageData = new List<TabletUsageData>();
                sortedTabletUsageData = _tabletUsageService.SortTablets(tabletUsageData);

                List<TabletUsageSummary> tabletUsageSummary = new List<TabletUsageSummary>();

                tabletUsageSummary = _tabletUsageService.GetTabletUsageSummary(sortedTabletUsageData);

                return Ok(tabletUsageSummary);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occured while getting battery usage: {ex.Message} ");
                return StatusCode(500, "Error occured while getting tablet usage");
            }
        }


    }
}

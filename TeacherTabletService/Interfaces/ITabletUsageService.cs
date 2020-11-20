using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherTabletService.Models;
using TeacherTabletService.ViewModels;

namespace TeacherTabletService.Interfaces
{
    public interface ITabletUsageService
    {
        List<TabletUsageSummary> GetTabletUsageSummary(List<TabletUsageData> data);
        List<TabletUsageData> SortTablets(List<TabletUsageData> data);
    }
}

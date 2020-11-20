using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherTabletService.Models;

namespace TeacherTabletService.Interfaces
{
    public interface IDataLoader
    {
        List<TabletUsageData> LoadDataFromFile(string fileName);
    }

}

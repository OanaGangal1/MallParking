using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAppUtility
    {
        double FreeParkingPeriod { get; init; }

        double AfterScanPeriod { get; init; }

        double FeePerHour { get; init; }

        double Tick { get; init; }
    }
}

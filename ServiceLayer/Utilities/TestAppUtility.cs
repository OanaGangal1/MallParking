using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Utilities
{
    public class TestAppUtility : IAppUtility
    {
        public double FreeParkingPeriod { get; init; }
        public double AfterScanPeriod { get; init; }
        public double FeePerHour { get; init; }
        public double Tick { get; init; }

        public TestAppUtility()
        {
            FreeParkingPeriod = 2 * 60 * 1000;

            AfterScanPeriod = 60 * 1000;

            FeePerHour = 5;

            Tick = 1000;
        }
    }
}

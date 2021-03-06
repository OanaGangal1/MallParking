using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Utilities
{
    public class AppUtility : IAppUtility
    {
        /// <summary>
        /// Free parking period expressed in milliseconds
        /// </summary>
        public double FreeParkingPeriod { get; init; }

        public double AfterScanPeriod { get; init; }

        public double FeePerHour { get; init; }

        public double Tick { get; init; }

        public AppUtility()
        {
            FreeParkingPeriod = 2 * 60 * 60 * 1000;

            AfterScanPeriod = 0.25 * 60 * 60 * 1000;

            FeePerHour = 5;

            Tick = 1000;
        }

    }
}

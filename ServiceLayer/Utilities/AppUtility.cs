using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utilities
{
    public static class AppUtility
    {
        /// <summary>
        /// Free parking period expressed in milliseconds
        /// </summary>
        public const double FreeParkingPeriod = 2 * 60 * 60 * 1000;

        public const double AfterScanPeriod = 0.25 * 60 * 60 * 1000;

        public const double FeePerHour = 5;

        public const double Tick = 1000;

    }
}

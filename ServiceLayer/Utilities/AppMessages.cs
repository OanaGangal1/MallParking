using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utilities
{
    public static class AppMessages
    {
        public static string BillChange(double change, double time)
        {
            var substring = Math.Abs(change) == 0
                ? ""
                : $"Change: {Math.Abs(change)}. ";

            return $"{substring}The bill was successfully paid. " +
                   $"You have {time / (60 * 1000):###} minutes to leave the parking lot";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using ServiceLayer.Utilities;

namespace ServiceLayer.Dtos
{
    public class BillInfoDto
    {
        public TimeRepresentation ElapsedTime { get; set; }
        public double Fee { get; set; }
        public string Message { get; set; }
    }
}

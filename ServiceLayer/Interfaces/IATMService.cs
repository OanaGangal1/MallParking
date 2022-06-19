using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IATMService : IScanTicket
    {
        public bool Pay(Ticket ticket);
    }
}

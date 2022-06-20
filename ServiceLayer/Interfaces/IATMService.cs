using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using ServiceLayer.Dtos;

namespace ServiceLayer.Interfaces
{
    public interface IATMService : IScanTicket
    {
        public FeeDto Pay(Ticket ticket, double fee);
    }
}

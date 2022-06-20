using DataLayer.Entities;
using ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IBillingService
    {
        BillInfoDto GetBillInfo(Ticket ticket);
    }
}

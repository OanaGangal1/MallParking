using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IBillService
    {
        Bill CreateBill(Guid ticketId);
        Bill CreateBill();
        Bill GetBill(Guid ticketId);
        bool PayBill(Bill bill, double fee);
        bool Charge(Bill bill, double fee);
        void AttachBill(Ticket ticket, Bill bill);
        void RemoveBill(Ticket ticket, Bill bill);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Bill CreateBill(Guid ticketId)
        {
            var bill = CreateBill();
            bill.TicketId = ticketId;
            _unitOfWork.Bills.Update(bill);
            return bill;
        }

        public Bill CreateBill()
        {
            var bill = _unitOfWork.Bills.New();
            return bill;
        }

        public Bill GetBill(Guid ticketId) => _unitOfWork.Bills.GetByTicketId(ticketId);

        public bool Charge(Bill bill, double fee)
        {
            bill.PaidAt = null;
            bill.Fee = fee;
            return _unitOfWork.Bills.Update(bill);
        }
        
        public bool PayBill(Bill bill, double fee)
        {
            if(bill == null)
                return false;

            bill.Fee -= fee;

            if(bill.Fee <= 0.0)
                bill.PaidAt = DateTime.UtcNow;

            return _unitOfWork.Bills.Update(bill);
        }

        public void AttachBill(Ticket ticket, Bill bill)
        {
            ticket.ActiveBill = bill;
            ticket.BillId = bill.Id;
            _unitOfWork.Tickets.Update(ticket);
        }

        public void RemoveBill(Ticket ticket, Bill bill)
        {
            if (ticket.BillId == bill.Id)
            {
                ticket.ActiveBill = null;
                ticket.BillId = Guid.Empty;
            }

            _unitOfWork.Tickets.Update(ticket);
        }
    }
}

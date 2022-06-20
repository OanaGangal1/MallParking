using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Enums;
using ServiceLayer.Dtos;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;
using ServiceLayer.Utilities;

namespace ServiceLayer.Services
{
    public class ATMService : IATMService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBillingService _billingService;
        private readonly IBillService _billService;
        private readonly ITicketService _ticketService;
        private readonly IAppUtility _appUtility;

        public ATMService(IUnitOfWork unitOfWork, 
            IBillingService billingService,
            IBillService billService,
            ITicketService ticketService,
            IAppUtility appUtility)
        {
            _unitOfWork = unitOfWork;
            _billingService = billingService;
            _billService = billService;
            _ticketService = ticketService;
            _appUtility = appUtility;
        }

        public BillInfoDto Scan(Ticket ticket)
        {
            var billInfo = _billingService.GetBillInfo(ticket);
            
            var bill = _billService.GetBill(ticket.Id);
            if (bill == null)
                throw new Exception(ErrorMessage.ServerError);
            
            _ticketService.ATMScanTicket(ticket);
            
            if (billInfo.Fee == 0.0)
            {
                _ticketService.SetStatus(ticket, TicketStatus.Ready);
                if (bill.PaidAt == null)
                {
                    bill.PaidAt = DateTime.UtcNow;
                    _unitOfWork.Bills.Update(bill);
                }
            }
            else
            {
                _billService.Charge(bill, billInfo.Fee);
            }
            
            return billInfo;
        }

        public FeeDto Pay(Ticket ticket, double fee)
        {
            if (ticket.ATMScannedAt == null)
                throw new BadRequestException(ErrorMessage.TicketNotATMScanned);

            var bill = _billService.GetBill(ticket.Id);
            if (bill == null)
                throw new Exception(ErrorMessage.ServerError);

            if (bill.PaidAt != null)
                throw new AppException(ErrorMessage.TicketAlreadyPaid);

            var remainingFee = bill.Fee - fee;

            _billService.PayBill(bill, fee);
            if(remainingFee <= 0)
                _ticketService.SetStatus(ticket, TicketStatus.Ready);

            return new FeeDto
            {
                RemainingFee = remainingFee,
                Message = remainingFee <= 0 ? AppMessages.BillChange(remainingFee, _appUtility.AfterScanPeriod) : ""
            };
        }
    }
}

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

        public ATMService(IUnitOfWork unitOfWork, 
            IBillingService billingService,
            IBillService billService,
            ITicketService ticketService)
        {
            _unitOfWork = unitOfWork;
            _billingService = billingService;
            _billService = billService;
            _ticketService = ticketService;
        }
        public BillInfoDto Scan(Ticket ticket)
        {
            var billInfo = _billingService.GetBillInfo(ticket);

            if (ticket.Status != TicketStatus.Active)
                throw new BadRequestException(ErrorMessage.TicketInactive);

            var bill = _billService.GetBill(ticket.Id);
            if (bill == null)
                throw new Exception(ErrorMessage.ServerError);

            _ticketService.ATMScanTicket(ticket);
            
            if (billInfo.Fee == 0.0)
            {
                _billService.PayBill(bill, 0.0);
            }
            else
            {
                _billService.Charge(bill, billInfo.Fee);
            }
            
            return billInfo;
        }

        public FeeDto Pay(Ticket ticket, double fee)
        {
            if (ticket.Status != TicketStatus.Active)
                throw new BadRequestException(ErrorMessage.TicketInactive);

            var bill = _billService.GetBill(ticket.Id);
            if (bill == null)
                throw new Exception(ErrorMessage.ServerError);

            if (bill.PaidAt != null)
                throw new AppException(ErrorMessage.TicketAlreadyPaid);

            var remainingFee = bill.Fee - fee;

            _billService.PayBill(bill, fee);

            return new FeeDto
            {
                RemainingFee = remainingFee,
                Message = AppMessages.BillChange(remainingFee)
            };
        }
    }
}

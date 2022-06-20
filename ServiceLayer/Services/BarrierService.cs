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
    public class BarrierService : IBarrierService
    {
        private readonly IBillingService _billingService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBillService _billService;
        private readonly ITicketService _ticketService;
        private readonly IAppUtility _appUtility;

        public BarrierService(IBillingService billingService, 
            IUnitOfWork unitOfWork, 
            IBillService billService,
            ITicketService ticketService,
            IAppUtility appUtility)
        {
            _billingService = billingService;
            _unitOfWork = unitOfWork;
            _billService = billService;
            _ticketService = ticketService;
            _appUtility = appUtility;
        }

        public BillInfoDto Scan(Ticket ticket)
        {
            if (ticket.ATMScannedAt == null)
                throw new BadRequestException(ErrorMessage.TicketNotATMScanned);

            var billInfo = _billingService.GetBillInfo(ticket);
            var bill = _billService.GetBill(ticket.Id);

            if (bill == null)
                throw new Exception(ErrorMessage.ServerError);

            _ticketService.BarrierScanTicket(ticket);

            if (bill.Fee == 0.0 && billInfo.Fee == 0.0)
            {
                _ticketService.CloseTicket(ticket);
                _billService.RemoveBill(ticket, bill);
                return billInfo;
            }

            if (bill.PaidAt != null && billInfo.Fee != 0)
            {
                _ticketService.ActivateTicket(ticket);
                _billService.Charge(bill, billInfo.Fee);
                throw new AppException(ErrorMessage.TicketUnpaidAfterScan(_appUtility.AfterScanPeriod));
            }
            
            throw new AppException(ErrorMessage.TicketUnpaid(billInfo.Fee));
        }
    }
}

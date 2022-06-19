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

        public ATMService(IUnitOfWork unitOfWork, IBillingService billingService)
        {
            _unitOfWork = unitOfWork;
            _billingService = billingService;
        }
        public BillInfoDto Scan(Ticket ticket)
        {
            var billInfo = _billingService.GetBillInfo(ticket);

            if (ticket.Status != TicketStatus.Active)
                throw new BadRequestException(ErrorMessage.TicketInactive);

            ticket.Scan();

            if (billInfo.Fee == 0.0)
                ticket.Pay();
            else
                ticket.Fee = billInfo.Fee;

            _unitOfWork.Tickets.Update(ticket);

            return billInfo;
        }

        public bool Pay(Ticket ticket)
        {
            if (ticket.Status != TicketStatus.Active)
                throw new BadRequestException(ErrorMessage.TicketInactive);

            ticket.Pay();
            return _unitOfWork.Tickets.Update(ticket);
        }
    }
}

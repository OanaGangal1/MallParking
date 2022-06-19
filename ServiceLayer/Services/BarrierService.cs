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

        public BarrierService(IBillingService billingService, IUnitOfWork unitOfWork)
        {
            _billingService = billingService;
            _unitOfWork = unitOfWork;
        }

        public BillInfoDto Scan(Ticket ticket)
        {
            if (ticket.Status == TicketStatus.Closed)
                throw new BadRequestException(ErrorMessage.TicketUsed);

            var billInfo = _billingService.GetBillInfo(ticket);

            if (ticket.Fee == 0.0 && billInfo.Fee == 0.0)
            { 
                ticket.Close();
                _unitOfWork.Tickets.Update(ticket);
                return billInfo;
            }

            if (ticket.Status == TicketStatus.Paid && billInfo.Fee != 0)
            {
                ticket.Status = TicketStatus.Active;
                _unitOfWork.Tickets.Update(ticket);
                throw new BadRequestException(ErrorMessage.TicketUnpaidAfterScan());
            }
            
            throw new BadRequestException(ErrorMessage.TicketUnpaid(billInfo.Fee));
        }
    }
}

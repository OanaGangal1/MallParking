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
    public interface ITicketService
    {
        TicketDto NewTicket();
        Ticket GetTicket(string code);
        IEnumerable<TicketDto> GetTickets();
        bool ActivateTicket(Ticket ticket);
        bool CloseTicket(Ticket ticket);
        bool ATMScanTicket(Ticket ticket);
        bool BarrierScanTicket(Ticket ticket);
        IEnumerable<TicketDto> GetClosedTickets();
    }

    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBillingService _billingService;
        private readonly IBillService _billService;

        public TicketService(IUnitOfWork unitOfWork, 
            IBillingService billingService,
            IBillService billService)
        {
            _unitOfWork = unitOfWork;
            _billingService = billingService;
            _billService = billService;
        }

        public TicketDto NewTicket()
        {
            var ticket = _unitOfWork.Tickets.New();
            var bill = _billService.CreateBill(ticket.Id);
            _billService.AttachBill(ticket, bill);
            return new TicketDto
            {
                Code = ticket.Code,
                Status = Enum.GetName(ticket.Status),
                CreatedAt = ticket.CreatedAt
            };
        }
        
        public Ticket GetTicket(string code) => _unitOfWork.Tickets.GetByCode(code);

        public IEnumerable<TicketDto> GetTickets()
        { 
            var tickets = _unitOfWork.Tickets.GetActiveTickets();

            return tickets
                .Select(ticket => new TicketDto
                {
                    Code = ticket.Code,
                    Status = Enum.GetName(ticket.Status),
                    CreatedAt = ticket.CreatedAt
                })
                .ToList();
        }

        public IEnumerable<TicketDto> GetClosedTickets()
        {
            var tickets = _unitOfWork.Tickets.GetAll();

            return tickets
                .Select(ticket => new TicketDto
                {
                    Code = ticket.Code,
                    Status = Enum.GetName(ticket.Status),
                    CreatedAt = ticket.CreatedAt
                })
                .ToList();
        }

        public bool ActivateTicket(Ticket ticket)
        {
            ticket.Status = TicketStatus.Active;
            return _unitOfWork.Tickets.Update(ticket);
        }

        public bool CloseTicket(Ticket ticket)
        {
            ticket.Status = TicketStatus.Closed;
            return _unitOfWork.Tickets.Update(ticket);
        }

        public bool ATMScanTicket(Ticket ticket)
        {
            ticket.ATMScannedAt = DateTime.UtcNow;
            return _unitOfWork.Tickets.Update(ticket);
        }

        public bool BarrierScanTicket(Ticket ticket)
        {
            ticket.BarrierScannedAt = DateTime.UtcNow;
            return _unitOfWork.Tickets.Update(ticket);
        }
    }
}

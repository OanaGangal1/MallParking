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
using ServiceLayer.Utilities;

namespace ServiceLayer.Services
{
    public interface ITicketService
    {
        TicketDto NewTicket();
        Ticket GetTicket(string code);
        IEnumerable<TicketDto> GetTickets();
    }

    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBillingService _billingService;

        public TicketService(IUnitOfWork unitOfWork, IBillingService billingService)
        {
            _unitOfWork = unitOfWork;
            _billingService = billingService;
        }

        public TicketDto NewTicket()
        {
            var ticket = _unitOfWork.Tickets.New();
            return new TicketDto { Code = ticket.Code, CreatedAt = ticket.CreatedAt };
        }

        public Ticket GetTicket(string code) => _unitOfWork.Tickets.GetByCode(code);

        public IEnumerable<TicketDto> GetTickets()
        { 
            var tickets = _unitOfWork.Tickets.GetAll();

            return tickets
                .Select(ticket => new TicketDto
                {
                    Code = ticket.Code, 
                    CreatedAt = ticket.CreatedAt
                })
                .ToList();
        }
    }
}

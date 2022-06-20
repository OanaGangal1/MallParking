using DataLayer.Entities;
using DataLayer.Enums;
using ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ITicketService
    {
        TicketDto NewTicket();
        Ticket GetTicket(string code);
        IEnumerable<TicketDto> GetTickets();
        bool ATMScanTicket(Ticket ticket);
        bool BarrierScanTicket(Ticket ticket);
        IEnumerable<TicketDto> GetAllTickets();
        Ticket GetActiveTicket(string code);
        bool SetStatus(Ticket ticket, TicketStatus status);
    }
}

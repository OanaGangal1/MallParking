using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Enums;
using DataLayer.Sets;

namespace DataLayer.Repos
{
    public interface ITicketRepo : IBaseRepo<Ticket>
    {
        Ticket GetByCode(string code);
        IEnumerable<Ticket> GetClosedTickets();
        IEnumerable<Ticket> GetActiveTickets();
        Ticket GetActiveByCode(string code);
    }
    public class TicketRepo : BaseRepo<Ticket>, ITicketRepo
    {
        public Ticket GetByCode(string code) => Set<Ticket>
            .Where(x => x.Status != TicketStatus.Closed && x.Code == code)
            .FirstOrDefault();

        public Ticket GetActiveByCode(string code) => Set<Ticket>
            .Where(x => x.Status == TicketStatus.Active && x.Code == code)
            .FirstOrDefault();

        public IEnumerable<Ticket> GetClosedTickets() => Set<Ticket>.Where(x => x.Status == TicketStatus.Closed);

        public IEnumerable<Ticket> GetActiveTickets() => Set<Ticket>.Where(x => x.Status != TicketStatus.Closed);
    }
}

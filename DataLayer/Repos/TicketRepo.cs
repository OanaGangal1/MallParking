using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Sets;

namespace DataLayer.Repos
{
    public interface ITicketRepo : IBaseRepo<Ticket>
    {
        Ticket GetByCode(string code);
    }
    public class TicketRepo : BaseRepo<Ticket>, ITicketRepo
    {
        public Ticket GetByCode(string code) => Set<Ticket>
            .Where(x => x.Code == code)
            .FirstOrDefault();
    }
}

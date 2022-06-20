using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Sets;

namespace DataLayer.Repos
{
    public interface IBillRepo : IBaseRepo<Bill>
    {
        Bill GetByTicketId(Guid ticketId);
    }

    public class BillRepo : BaseRepo<Bill>, IBillRepo
    {
        public Bill GetByTicketId(Guid ticketId) => Set<Bill>
            .Where(x => x.TicketId == ticketId)
            .FirstOrDefault();
    }
}

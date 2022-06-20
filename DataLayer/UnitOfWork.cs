using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repos;

namespace DataLayer
{
    public interface IUnitOfWork
    {
        ITicketRepo Tickets { get; }
        IBillRepo Bills { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        public ITicketRepo Tickets { get; }
        public IBillRepo Bills { get; }

        public UnitOfWork(ITicketRepo ticketRepo, IBillRepo billRepo)
        {
            Tickets = ticketRepo;
            Bills = billRepo;
        }
    }
}

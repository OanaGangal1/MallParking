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
    }
    public class UnitOfWork : IUnitOfWork
    {
        public ITicketRepo Tickets { get; }

        public UnitOfWork(ITicketRepo ticketRepo)
        {
            Tickets = ticketRepo;
        }
    }
}

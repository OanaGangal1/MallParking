using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Bill : BaseEntity
    {
        public double Fee { get; set; }
        public DateTime? PaidAt { get; set; }
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}

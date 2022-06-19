using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;

namespace DataLayer.Entities
{
    public class Ticket : BaseEntity
    {
        public string Code { get; init; }
        public DateTime? ScannedAt { get; set; }
        public TicketStatus Status { get; set; }
        public double Fee { get; set; }

        public Ticket()
        {
            Code = Guid.NewGuid().ToString().Substring(0, 9);
        }

        public void Scan()
        {
            ScannedAt = DateTime.UtcNow;
        }

        public void Pay()
        {
            Status = TicketStatus.Paid;
            Fee = 0.0;
        }

        public void Close()
        {
            Status = TicketStatus.Closed;
        }
    }
}

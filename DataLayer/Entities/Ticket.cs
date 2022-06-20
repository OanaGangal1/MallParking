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
        public DateTime? ATMScannedAt { get; set; }
        public DateTime? BarrierScannedAt { get; set; }
        public TicketStatus Status { get; set; }
        public Guid BillId { get; set; }
        public Bill ActiveBill { get; set; }

        public Ticket()
        {
            Code = Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}

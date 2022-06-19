using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime ArchivedAt { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}

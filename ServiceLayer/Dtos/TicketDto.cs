using DataLayer.Enums;

namespace ServiceLayer.Dtos
{
    public class TicketDto
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

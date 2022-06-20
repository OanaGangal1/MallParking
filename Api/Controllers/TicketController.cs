using ServiceLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using ServiceLayer.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("new")]
        public TicketDto CreateNew() => _ticketService.NewTicket();

        [HttpGet("allActive")]
        public IEnumerable<TicketDto> GetAll() => _ticketService.GetTickets();

        [HttpGet("activeByCode")]
        public TicketDto GetByCode(string code)
        {
            var ticket = _ticketService.GetTicket(code);
            if (ticket == null)
                throw new BadRequestException(ErrorMessage.NoTicket);

            return new TicketDto
            {
                Code = ticket.Code,
                Status = Enum.GetName(ticket.Status),
                CreatedAt = ticket.CreatedAt,
            };
        }

        [HttpGet("all")]
        public IEnumerable<TicketDto> GetAllClosed() => _ticketService.GetAllTickets();

    }
}

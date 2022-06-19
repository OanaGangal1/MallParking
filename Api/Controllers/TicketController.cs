using ServiceLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using ServiceLayer.Utilities;

namespace Api.Controllers
{
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("new")]
        public TicketDto CreateNew() => _ticketService.NewTicket();

        [HttpGet("all")]
        public IEnumerable<TicketDto> GetAll() => _ticketService.GetTickets();
    }
}

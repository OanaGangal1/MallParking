using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using ServiceLayer.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class BarrierController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IBarrierService _barrierService;

        public BarrierController(ITicketService ticketService, IBarrierService barrierService)
        {
            _ticketService = ticketService;
            _barrierService = barrierService;
        }

        [HttpGet("scan/{ticketCode}")]
        public BillInfoDto BarrierScan(string ticketCode)
        {
            var ticket = _ticketService.GetTicket(ticketCode);
            if (ticket == null)
                throw new BadRequestException(ErrorMessage.NoTicket);

            return _barrierService.Scan(ticket);
        }
    }
}

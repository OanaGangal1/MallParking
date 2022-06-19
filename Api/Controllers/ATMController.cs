using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using ServiceLayer.Utilities;

namespace Api.Controllers
{
    public class ATMController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IATMService _atmService;

        public ATMController(ITicketService ticketService, IATMService atmService)
        {
            _ticketService = ticketService;
            _atmService = atmService;
        }

        [HttpGet("scan")]
        public BillInfoDto ATMScan(string ticketCode)
        {
            var ticket = _ticketService.GetTicket(ticketCode);
            if (ticket == null)
                throw new BadRequestException(ErrorMessage.NoTicket);

            return _atmService.Scan(ticket);
        }

        [HttpGet("pay")]
        public bool Pay(string ticketCode)
        {
            var ticket = _ticketService.GetTicket(ticketCode);
            if (ticket == null)
                throw new BadRequestException(ErrorMessage.NoTicket);

            return _atmService.Pay(ticket);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using ServiceLayer.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ATMController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IATMService _atmService;

        public ATMController(ITicketService ticketService, IATMService atmService)
        {
            _ticketService = ticketService;
            _atmService = atmService;
        }

        [HttpGet("scan/{ticketCode}")]
        public BillInfoDto ATMScan(string ticketCode)
        {
            var ticket = _ticketService.GetActiveTicket(ticketCode);
            if (ticket == null)
                throw new BadRequestException(ErrorMessage.NoTicket);

            return _atmService.Scan(ticket);
        }

        [HttpPost("pay")]
        public FeeDto Pay(PayTicketDto payTicket)
        {
            var ticket = _ticketService.GetActiveTicket(payTicket.TicketCode);
            if (ticket == null)
                throw new BadRequestException(ErrorMessage.NoTicket);

            return _atmService.Pay(ticket, payTicket.Fee);
        }
    }
}

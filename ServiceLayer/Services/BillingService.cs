using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Enums;
using ServiceLayer.Dtos;
using ServiceLayer.Utilities;

namespace ServiceLayer.Services
{
    public interface IBillingService
    {
        BillInfoDto GetBillInfo(Ticket ticket);
    }

    public class BillingService : IBillingService
    {
        public BillInfoDto GetBillInfo(Ticket ticket)
        {
            var timeSpan = DateTime.UtcNow - ticket.CreatedAt;
            var elapsedMillisec = timeSpan.TotalMilliseconds;

            if (ticket.Status == TicketStatus.Paid)
            {
                timeSpan = DateTime.UtcNow - ticket.ScannedAt!.Value;

                if (timeSpan.TotalMilliseconds <= AppUtility.AfterScanPeriod + AppUtility.Tick)
                    return new BillInfoDto
                    {
                        ElapsedTime = new TimeRepresentation
                        {
                            Hours = timeSpan.Hours,
                            Minutes = timeSpan.Minutes,
                            Seconds = timeSpan.Seconds
                        },
                        Fee = 0.0
                    };
            }

            return new BillInfoDto
            {
                ElapsedTime = new TimeRepresentation
                {
                    Hours = timeSpan.Hours,
                    Minutes = timeSpan.Minutes,
                    Seconds = timeSpan.Seconds
                },
                Fee = CalculateFee(elapsedMillisec)
            };
        }

        private double CalculateFee(double milliseconds)
        {
            if (milliseconds <= AppUtility.FreeParkingPeriod + AppUtility.Tick)
                return 0.0;
            return AppUtility.FeePerHour * Math.Round(milliseconds / AppUtility.FreeParkingPeriod, MidpointRounding.AwayFromZero);
        }
    }
}

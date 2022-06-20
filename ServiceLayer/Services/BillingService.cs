using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Enums;
using ServiceLayer.Dtos;
using ServiceLayer.Interfaces;
using ServiceLayer.Utilities;

namespace ServiceLayer.Services
{
    public class BillingService : IBillingService
    {
        private readonly IAppUtility _appUtility;

        public BillingService(IAppUtility appUtility)
        {
            _appUtility = appUtility;
        }
        public BillInfoDto GetBillInfo(Ticket ticket)
        {
            var timeSpanCreatedAt = DateTime.UtcNow - ticket.CreatedAt;
            var elapsedMillisec = timeSpanCreatedAt.TotalMilliseconds;

            if (ticket.ActiveBill.PaidAt != null)
            {
                var timeSpan = DateTime.UtcNow - ticket.ActiveBill.PaidAt.Value;

                if (timeSpan.TotalMilliseconds <= _appUtility.AfterScanPeriod + _appUtility.Tick)
                    return new BillInfoDto
                    {
                        ElapsedTime = new TimeRepresentation
                        {
                            Hours = timeSpanCreatedAt.Hours,
                            Minutes = timeSpanCreatedAt.Minutes,
                            Seconds = timeSpanCreatedAt.Seconds
                        },
                        Fee = 0.0
                    };

                elapsedMillisec = elapsedMillisec < _appUtility.FreeParkingPeriod
                    ? _appUtility.FreeParkingPeriod + timeSpan.TotalMilliseconds
                    : elapsedMillisec;
            }

            return new BillInfoDto
            {
                ElapsedTime = new TimeRepresentation
                {
                    Hours = timeSpanCreatedAt.Hours,
                    Minutes = timeSpanCreatedAt.Minutes,
                    Seconds = timeSpanCreatedAt.Seconds
                },
                Fee = CalculateFee(elapsedMillisec)
            };
        }

        private double CalculateFee(double milliseconds)
        {
            if (milliseconds <= _appUtility.FreeParkingPeriod + _appUtility.Tick)
                return 0.0;
            return _appUtility.FeePerHour * Math.Round(milliseconds / _appUtility.FreeParkingPeriod, MidpointRounding.AwayFromZero);
        }
    }
}

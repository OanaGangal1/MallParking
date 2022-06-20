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
    public interface IBillingService
    {
        BillInfoDto GetBillInfo(Ticket ticket);
    }

    public class BillingService : IBillingService
    {
        private readonly IAppUtility _appUtility;

        public BillingService(IAppUtility appUtility)
        {
            _appUtility = appUtility;
        }
        public BillInfoDto GetBillInfo(Ticket ticket)
        {
            var timeSpan = DateTime.UtcNow - ticket.CreatedAt;
            var elapsedMillisec = timeSpan.TotalMilliseconds;

            if (ticket.ActiveBill.PaidAt != null)
            {
                timeSpan = DateTime.UtcNow - ticket.ATMScannedAt!.Value;

                if (timeSpan.TotalMilliseconds <= _appUtility.AfterScanPeriod + _appUtility.Tick)
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
            if (milliseconds <= _appUtility.FreeParkingPeriod + _appUtility.Tick)
                return 0.0;
            return _appUtility.FeePerHour * Math.Round(milliseconds / _appUtility.FreeParkingPeriod, MidpointRounding.AwayFromZero);
        }
    }
}

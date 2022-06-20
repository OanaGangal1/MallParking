namespace ServiceLayer.Utilities
{
    public static class ErrorMessage
    {
        public const string TicketNotATMScanned = "Please scan your ticket at ATM first!";
        public const string NoTicket = "Ticket does not exist in our records!";
        public const string TicketInactive = "Ticket not active";
        public const string TicketUsed = "Tick already used!";
        public const string ServerError = "Server Error!";
        public const string TicketAlreadyPaid = "Ticket was already paid1";

        public static string TicketUnpaid(double fee) => $"Ticket fee unpaid. You have to pay {fee} ron!";
        public static string TicketUnpaidAfterScan(double scanPeriod) => $"You have passed the {scanPeriod / (60 * 1000):###} minutes after scan limit." +
                                                                  " Please return to the ATM!";
    }
}

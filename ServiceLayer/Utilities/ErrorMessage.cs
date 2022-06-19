namespace ServiceLayer.Utilities
{
    public static class ErrorMessage
    {
        public const string NoTicket = "Ticket does not exist in our records!";
        public const string TicketInactive = "Ticket not active";
        public const string TicketUsed = "Tick already used!";

        public static string TicketUnpaid(double fee) => $"Ticket fee unpaid. You have to pay {fee} ron!";
        public static string TicketUnpaidAfterScan() => $"You have passed the {AppUtility.AfterScanPeriod / (60 * 1000):###} minutes after scan limit." +
                                                                  " Please return to the ATM!";
    }
}

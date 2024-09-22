namespace ByteMeAPI.Service.Models.Response
{
    public class BankDetailsResponse
    {
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string Branch { get; set; }
        public string BranchCode { get; set; }
        public string CardType { get; set; }
        public bool IsPrimary { get; set; }
    }
}

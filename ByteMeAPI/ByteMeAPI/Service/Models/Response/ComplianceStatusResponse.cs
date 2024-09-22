namespace ByteMeAPI.Service.Models.Response
{
    public class ComplianceStatusResponse
    {
        public string Status { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string ConfirmedBy { get; set; }
        public DateTime ConfirmedOn { get; set; }
        public int Months { get; set; }
    }
}

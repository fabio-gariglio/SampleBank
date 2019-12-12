namespace SampleBank.Domain.Services
{
    public class Transfer
    {
        public string SenderAccountNumber { get; set; }
        public string PayeeAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
    }
}
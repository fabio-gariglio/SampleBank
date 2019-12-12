namespace SampleBank.API.Contracts
{
    public class MakePaymentRequest
    {
        public string PayeeAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
    }
}
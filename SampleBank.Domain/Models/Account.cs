using System;

namespace SampleBank.Domain.Models
{
    public class Account
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }
        public bool Open { get; set; }
    }
}

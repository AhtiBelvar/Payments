using System;

namespace ClearBank.DeveloperTest.Payments;

public class MakePaymentRequest
{
    public string CreditorAccountNumber { get; set; }

    public string DebtorAccountNumber { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public PaymentScheme PaymentScheme { get; set; }
}

using ClearBank.DeveloperTest.Accounts;

namespace ClearBank.DeveloperTest.Payments.Schemes;

public interface IPaymentScheme
{
    PaymentScheme Scheme { get; }

    MakePaymentResult MakePayment(Account account, MakePaymentRequest request);
}
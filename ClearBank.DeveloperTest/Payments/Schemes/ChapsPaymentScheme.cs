using ClearBank.DeveloperTest.Accounts;

namespace ClearBank.DeveloperTest.Payments.Schemes;

public class ChapsPaymentScheme : IPaymentScheme
{
    public PaymentScheme Scheme => PaymentScheme.Chaps;

    public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
        => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps)
        && account.Status == AccountStatus.Live
            ? MakePaymentResult.Succeeded()
            : MakePaymentResult.Failure();
}
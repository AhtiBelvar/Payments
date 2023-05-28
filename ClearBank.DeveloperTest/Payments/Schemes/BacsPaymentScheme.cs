using ClearBank.DeveloperTest.Accounts;

namespace ClearBank.DeveloperTest.Payments.Schemes;

public class BacsPaymentScheme : IPaymentScheme
{
    public PaymentScheme Scheme => PaymentScheme.Bacs;

    public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
        => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs)
            ? MakePaymentResult.Succeeded()
            : MakePaymentResult.Failure();
}

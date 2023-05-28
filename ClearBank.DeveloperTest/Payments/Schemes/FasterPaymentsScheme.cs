using ClearBank.DeveloperTest.Accounts;

namespace ClearBank.DeveloperTest.Payments.Schemes;

public class FasterPaymentsScheme : IPaymentScheme
{
    public PaymentScheme Scheme => PaymentScheme.FasterPayments;

    public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
        => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments)
        && account.Balance >= request.Amount
            ? MakePaymentResult.Succeeded()
            : MakePaymentResult.Failure();
}
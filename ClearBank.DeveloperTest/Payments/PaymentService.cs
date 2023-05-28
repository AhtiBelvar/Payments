using ClearBank.DeveloperTest.Accounts;
using ClearBank.DeveloperTest.Accounts.Storage;

namespace ClearBank.DeveloperTest.Payments;

public class PaymentService : IPaymentService
{
    private readonly IAccountStore _accountStorage;

    public PaymentService(IAccountStore accountStorage)
    {
        _accountStorage = accountStorage;
    }

    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
        var result = new MakePaymentResult();

        if (_accountStorage == null)
        {
            // TODO Log error here
            return MakePaymentResult.Failure();
        }

        var account = _accountStorage.GetAccount(request.DebtorAccountNumber);

        if (account == null)
        {
            // TODO Log informational
            return MakePaymentResult.Failure();
        }

        switch (request.PaymentScheme)
        {
            case PaymentScheme.Bacs:
                if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                {
                    return MakePaymentResult.Failure();
                }
                break;

            case PaymentScheme.FasterPayments:
                if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
                {
                    return MakePaymentResult.Failure();
                }
                else if (account.Balance < request.Amount)
                {
                    return MakePaymentResult.Failure();
                }
                break;

            case PaymentScheme.Chaps:
                if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                {
                    return MakePaymentResult.Failure();
                }
                else if (account.Status != AccountStatus.Live)
                {
                    return MakePaymentResult.Failure();
                }
                break;
        }

        account.Balance -= request.Amount;
        _accountStorage.UpdateAccount(account);

        return MakePaymentResult.Succeeded();
    }
}

using ClearBank.DeveloperTest.Accounts;
using ClearBank.DeveloperTest.Accounts.Storage;

namespace ClearBank.DeveloperTest.Payments;

public class PaymentService : IPaymentService
{
    private readonly IAccountStore _accountsStorage;

    public PaymentService(IAccountStore accountsStorage)
    {
        _accountsStorage = accountsStorage;
    }

    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
        var result = new MakePaymentResult();

        if (_accountsStorage == null)
        {
            // TODO Log error here
            return BuildFailureResult();
        }

        var account = _accountsStorage?.GetAccount(request.DebtorAccountNumber);

        if (account == null)
        {
            // TODO Log informational
            return BuildFailureResult();
        }

        switch (request.PaymentScheme)
        {
            case PaymentScheme.Bacs:
                if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                {
                    result.Success = false;
                }
                break;

            case PaymentScheme.FasterPayments:
                if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
                {
                    result.Success = false;
                }
                else if (account.Balance < request.Amount)
                {
                    result.Success = false;
                }
                break;

            case PaymentScheme.Chaps:
                if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                {
                    result.Success = false;
                }
                else if (account.Status != AccountStatus.Live)
                {
                    result.Success = false;
                }
                break;
        }

        if (result.Success)
        {
            account.Balance -= request.Amount;
            _accountsStorage.UpdateAccount(account);
        }

        return result;
    }

    private MakePaymentResult BuildFailureResult()
    {
        return new MakePaymentResult { Success = false };
    }
}

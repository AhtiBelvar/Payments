using System.Collections.Generic;
using System.Linq;
using ClearBank.DeveloperTest.Accounts;
using ClearBank.DeveloperTest.Accounts.Storage;
using ClearBank.DeveloperTest.Payments.Schemes;

namespace ClearBank.DeveloperTest.Payments;

public class PaymentService : IPaymentService
{
    private readonly IAccountStore _accountStorage;
    private readonly IDictionary<PaymentScheme, IPaymentScheme> _paymentSchemes;

    public PaymentService(
        IAccountStore accountStorage,
        IEnumerable<IPaymentScheme> paymentSchemes)
    {
        _accountStorage = accountStorage;
        _paymentSchemes = paymentSchemes.ToDictionary(s => s.Scheme);
    }

    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
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

        if (!_paymentSchemes.TryGetValue(request.PaymentScheme, out var paymentScheme))
        {
            // TODO Log informational
            return MakePaymentResult.Failure();
        }

        var result = paymentScheme.MakePayment(account, request);
        if (!result.Success)
        {
            return MakePaymentResult.Failure();
        }

        account.Balance -= request.Amount;
        _accountStorage.UpdateAccount(account);

        return MakePaymentResult.Succeeded();
    }
}

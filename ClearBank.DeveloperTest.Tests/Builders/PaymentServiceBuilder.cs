using ClearBank.DeveloperTest.Accounts.Storage;
using ClearBank.DeveloperTest.Payments;
using ClearBank.DeveloperTest.Payments.Schemes;
using ClearBank.DeveloperTest.Tests.Stubs;

namespace ClearBank.DeveloperTest.Tests.Builders;

public class PaymentServiceBuilder
{
    private IPaymentScheme[] _paymentSchemes = new IPaymentScheme[]
    {
        new BacsPaymentScheme(),
        new ChapsPaymentScheme(),
        new FasterPaymentsScheme()
    };
    private StubAccountStore _accountStore = new StubAccountStore();

    public PaymentServiceBuilder WithAccountStore(StubAccountStore store)
    {
        _accountStore = store;
        return this;
    }

    public PaymentServiceBuilder WithPaymentSchemes(params IPaymentScheme[] paymentSchemes)
    {
        _paymentSchemes = paymentSchemes;
        return this;
    }

    public PaymentService Build()
    {
        return new PaymentService(_accountStore, _paymentSchemes);
    }
}

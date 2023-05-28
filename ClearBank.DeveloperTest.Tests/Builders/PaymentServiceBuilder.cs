using ClearBank.DeveloperTest.Accounts.Storage;
using ClearBank.DeveloperTest.Payments;
using ClearBank.DeveloperTest.Tests.Stubs;

namespace ClearBank.DeveloperTest.Tests.Builders;

public class PaymentServiceBuilder
{
    private IAccountStore _accountStore = new StubAccountStore();

    public PaymentServiceBuilder WithAccountStore(IAccountStore store)
    {
        _accountStore = store;
        return this;
    }

    public PaymentService Build()
    {
        return new PaymentService(_accountStore);
    }
}
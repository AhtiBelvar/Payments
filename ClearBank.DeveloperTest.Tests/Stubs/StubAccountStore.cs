using System.Collections.Generic;
using System.Linq;
using ClearBank.DeveloperTest.Accounts;
using ClearBank.DeveloperTest.Accounts.Storage;

namespace ClearBank.DeveloperTest.Tests.Stubs;

public class StubAccountStore : IAccountStore
{
    public readonly Dictionary<string, Account> _accounts = new Dictionary<string, Account>();

    public StubAccountStore() { }

    public StubAccountStore(params Account[] accounts)
    {
        _accounts = accounts.ToDictionary(a => a.AccountNumber);
    }

    public Account GetAccount(string accountNumber)
        => _accounts.GetValueOrDefault(accountNumber);

    public void UpdateAccount(Account account)
        => _accounts[account.AccountNumber] = account;
}

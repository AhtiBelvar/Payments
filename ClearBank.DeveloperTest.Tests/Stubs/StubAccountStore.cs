using System.Collections.Generic;
using ClearBank.DeveloperTest.Accounts;
using ClearBank.DeveloperTest.Accounts.Storage;

namespace ClearBank.DeveloperTest.Tests.Stubs;

public class StubAccountStore : IAccountStore
{
    public readonly Dictionary<string, Account> _accounts = new Dictionary<string, Account>();

    public Account GetAccount(string accountNumber)
        => _accounts.GetValueOrDefault(accountNumber);

    public void UpdateAccount(Account account)
        => _accounts[account.AccountNumber] = account;
}
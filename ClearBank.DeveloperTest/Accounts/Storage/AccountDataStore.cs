﻿namespace ClearBank.DeveloperTest.Accounts.Storage;

public class AccountDataStore : IAccountStore
{
    public Account GetAccount(string accountNumber)
    {
        // Access database to retrieve account, code removed for brevity 
        return new Account();
    }

    public void UpdateAccount(Account account)
    {
        // Update account in database, code removed for brevity
    }
}

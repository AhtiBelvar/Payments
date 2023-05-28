namespace ClearBank.DeveloperTest.Accounts.Storage;

public interface IAccountsStore
{
    Account GetAccount(string accountNumber);

    void UpdateAccount(Account account);
}

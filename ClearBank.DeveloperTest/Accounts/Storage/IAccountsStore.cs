namespace ClearBank.DeveloperTest.Accounts.Storage;

public interface IAccountStore
{
    Account GetAccount(string accountNumber);

    void UpdateAccount(Account account);
}

using Account = Program.AccountRecord;
class AccountDatabase
{
    public AccountDatabase(Account[] accounts)
    {
        Accounts = accounts;
    }

    public Account[] Accounts { get; }
}
class LevelDatabase
{
    
}
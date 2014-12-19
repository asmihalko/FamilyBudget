using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FamilyBudget
{
    public class AccountManager
    {
        private static Context context = new Context();

        public Context Context
        {
            get
            {
                return context;
            }
        }

        public AccountManager()
        {
        }
        public void InitializeData()
        {
            context.Accounts.Load();
        }

        public void DeleteAccount(Account account)
        {
            context.Accounts.Local.Remove(account);
            context.SaveChanges();
        }

        public void AddAccount(string accountName)
        {
            context.Accounts.Local.Add(new Account(accountName));
            context.SaveChanges();
        }

        public void EditAccount(Account account, string accountName)
        {
            context.Accounts.Local.ElementAt(context.Accounts.Local.IndexOf(account)).Name = accountName;
            context.SaveChanges();
            IncomeManager incomeManager = new IncomeManager();
            ExpenseManager expenseManager = new ExpenseManager();
            TransferManager transferManager = new TransferManager();


            foreach (var income in incomeManager.Context.Operations.Local)
            {
                if (income.AccountToPutId == context.Accounts.Local.ElementAt(context.Accounts.Local.IndexOf(account)).Id)
                {
                    incomeManager.Context.Entry(income).Reference(op => op.AccountToPut).Load();
                    incomeManager.Context.Operations.Local.ElementAt(incomeManager.Context.Operations.Local.IndexOf(income))
                        .OnPropertyChanged("AccountToPutToString");
                }
            }

            incomeManager.Context.SaveChanges();
        }
    }
}

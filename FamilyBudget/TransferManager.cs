using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;

namespace FamilyBudget
{
    public class TransferManager : IOperationManager
    {
        private static Context context = new Context();
        AccountManager accountManager = new AccountManager();

        public Context Context
        {
            get
            {
                return context;
            }
        }
        public TransferManager()
        {
        }

        //public double Amount { get; set; }

        public void InitializeData()
        {
            context.Operations
                .Where(op => op.OperationType == "transfer")
                .Include(op => op.AccountToWithdraw)
                .Include(op => op.Currency)
                .Include(op => op.AccountToPut)
                .Load();
        }
        public void AddOperation(double sum, DateTime date, Account accountToPut, Account accountToWithdraw, Currency currency, Category category, string comment)
        {
            
            Operation operation = new Operation("transfer", sum, date, accountToPut, accountToWithdraw, currency, category, comment);

            foreach (Operation op in context.Operations.Local)
            {
                if (operation == op)
                    throw new Exception("Такая операция уже существует!");
            }
            context.Operations.Local.Add(operation);
            context.Entry(operation).Reference(op => op.Category).Load();
            context.Entry(operation).Reference(op => op.Currency).Load();
            context.Entry(operation).Reference(op => op.AccountToWithdraw).Load();
            context.Entry(operation).Reference(op => op.AccountToPut).Load();


            switch (currency.Name)
            {
                case "RUB":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToWithdraw.Id).Single().RubSum -= operation.Sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToPut.Id).Single().RubSum += operation.Sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToWithdraw.Id).Single().DolSum -= operation.Sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToPut.Id).Single().DolSum += operation.Sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToWithdraw.Id).Single().EuroSum -= operation.Sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToPut.Id).Single().EuroSum += operation.Sum;
                    break;
            }

            context.SaveChanges();
            accountManager.Context.SaveChanges();
        
        }

        public void DeleteOperation(Operation operation)
        {
            switch (operation.Currency.Name)
            {
                case "RUB":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().RubSum += operation.Sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().RubSum -= operation.Sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().DolSum += operation.Sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().DolSum -= operation.Sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().EuroSum += operation.Sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().EuroSum -= operation.Sum;
                    break;
            }

            context.Operations.Local.Remove(operation);

            context.SaveChanges();
            accountManager.Context.SaveChanges();
        }

        public void EditOperation(Operation operation, double sum, DateTime date, Account accountToPut, Account accountToWithdraw, Currency currency,
            Category category, string comment)
        {
            switch (operation.Currency.Name)
            {
                case "RUB":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().RubSum += operation.Sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().RubSum -= operation.Sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().DolSum += operation.Sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().DolSum -= operation.Sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().EuroSum += operation.Sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().EuroSum -= operation.Sum;
                    break;
            }

            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).Sum = sum;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).Date = date;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).AccountToWithdrawId = accountToWithdraw.Id;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).AccountToPutId = accountToPut.Id;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).CurrencyId = currency.Id;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).Comment = comment;

            context.Entry(operation).Reference(op => op.Category).Load();
            context.Entry(operation).Reference(op => op.Currency).Load();
            context.Entry(operation).Reference(op => op.AccountToWithdraw).Load();
            context.Entry(operation).Reference(op => op.AccountToPut).Load();

            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("AccountToWithdrawToString");
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("AccountToPutToString");
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("CurrencyToString");
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("CategoryToString");

            switch (currency.Name)
            {
                case "RUB":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToWithdraw.Id).Single().RubSum -= sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToPut.Id).Single().RubSum += sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToWithdraw.Id).Single().DolSum -= sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToPut.Id).Single().DolSum += sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToWithdraw.Id).Single().EuroSum -= sum;
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToPut.Id).Single().EuroSum += sum;
                    break;
            }

            context.SaveChanges();
            accountManager.Context.SaveChanges();
        }
    }
}

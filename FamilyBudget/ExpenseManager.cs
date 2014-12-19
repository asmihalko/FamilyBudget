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
   public class ExpenseManager : IOperationManager
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

        public ExpenseManager()
        {
        }

        //public double Amount { get; set; }

        public void InitializeData()
        {
            context.Operations
                .Where(op => op.OperationType == "expense")
                .Include(op => op.Category)
                .Include(op => op.Currency)
                .Include(op => op.AccountToWithdraw)
                .Load();
        }
        public void AddOperation(double sum, DateTime date, Account accountToPut, Account accountToWithdraw, Currency currency, Category category, string comment)
        {
            Operation operation = new Operation("expense", sum, date, null, accountToWithdraw, currency, category, comment);

            foreach (Operation op in context.Operations.Local)
                {
                    if (operation == op)
                        throw new Exception("Такая операция уже существует!");
                }

            context.Operations.Local.Add(operation);
            context.Entry(operation).Reference(op => op.Category).Load();
            context.Entry(operation).Reference(op => op.Currency).Load();
            context.Entry(operation).Reference(op => op.AccountToWithdraw).Load();


            switch (currency.Name)
            {
                case "RUB":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToWithdraw.Id).Single().RubSum -= operation.Sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToWithdraw.Id).Single().DolSum -= operation.Sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToWithdraw.Id).Single().EuroSum -= operation.Sum;
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
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().DolSum += operation.Sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().EuroSum += operation.Sum;
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
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().DolSum += operation.Sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().EuroSum += operation.Sum;
                    break;
            }

            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).Sum = sum;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).Date = date;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).AccountToWithdrawId = accountToWithdraw.Id;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).CurrencyId = currency.Id;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).CategoryId = category.Id;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).Comment = comment;

            context.Entry(operation).Reference(op => op.Category).Load();
            context.Entry(operation).Reference(op => op.Currency).Load();
            context.Entry(operation).Reference(op => op.AccountToWithdraw).Load();

            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("AccountToWithdrawToString");
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("CurrencyToString");
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("CategoryToString");

            switch (currency.Name)
            {
                case "RUB":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().RubSum -= sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().DolSum -= sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToWithdraw.Id).Single().EuroSum -= sum;
                    break;
            }

            context.SaveChanges();
            accountManager.Context.SaveChanges();
        }
    }
}

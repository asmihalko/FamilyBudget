using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Windows;

namespace FamilyBudget
{
    public class IncomeManager : IOperationManager 
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

        public IncomeManager()
        {
        }

        //public double Amount { get; set; } потом надо вывести куда-то общую суммму доходов (мб автоматом брать опр переиод, чтоб не выкатывать базу за 5 лет)

        public void InitializeData()
        {
            context.Operations
                .Where(op => op.OperationType == "income")
               .Include(op => op.Category)
               .Include(op => op.Currency)
               .Include(op => op.AccountToPut)
               .Load();
        }
        public void AddOperation(double sum, DateTime date, Account accountToPut, Account accountToWithdraw, Currency currency, Category category, string comment)
        {
            
            Operation operation = new Operation("income", sum, date, accountToPut, null, currency, category, comment);

            foreach (Operation op in context.Operations.Local)
            {
                if (operation.AccountToPutId == op.AccountToPutId && operation.Sum == op.Sum
                    && operation.CurrencyId == op.CurrencyId && operation.CategoryId == op.CategoryId
                    && operation.Date.Equals(op.Date) && operation.Comment == op.Comment)
                    throw new Exception("Такая операция уже существует!");
            }
            context.Operations.Local.Add(operation);
            context.Entry(operation).Reference(op => op.Currency).Load();
            context.Entry(operation).Reference(op => op.Category).Load();
            context.Entry(operation).Reference(op => op.AccountToPut).Load();


            switch (currency.Name)
            {
                case "RUB":
                    accountManager.Context.Accounts.Local.ElementAt(accountManager.Context.Accounts.Local.IndexOf(accountToPut)).RubSum += operation.Sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local.ElementAt(accountManager.Context.Accounts.Local.IndexOf(accountToPut)).DolSum += operation.Sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local.ElementAt(accountManager.Context.Accounts.Local.IndexOf(accountToPut)).EuroSum += operation.Sum;
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
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().RubSum -= operation.Sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().DolSum -= operation.Sum;
                    break;
                case "EURO":
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
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().RubSum -= operation.Sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().DolSum -= operation.Sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == operation.AccountToPut.Id).Single().EuroSum -= operation.Sum;
                    break;
            }


            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).Sum = sum;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).Date = date;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).AccountToPutId = accountToPut.Id;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).CurrencyId = currency.Id;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).CategoryId = category.Id;
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).Comment = comment;

            context.Entry(operation).Reference(op => op.Currency).Load();
            context.Entry(operation).Reference(op => op.AccountToPut).Load();
            context.Entry(operation).Reference(op => op.Category).Load();

            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("AccountToPutToString");
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("CurrencyToString");
            context.Operations.Local.ElementAt(context.Operations.Local.IndexOf(operation)).OnPropertyChanged("CategoryToString");

            switch (currency.Name)
            {
                case "RUB":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToPut.Id).Single().RubSum += sum;
                    break;
                case "USD":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToPut.Id).Single().DolSum += sum;
                    break;
                case "EURO":
                    accountManager.Context.Accounts.Local
                        .Where(acc => acc.Id == accountToPut.Id).Single().EuroSum += sum;
                    break;
            }

            context.SaveChanges();
            accountManager.Context.SaveChanges();

        }
    }
}

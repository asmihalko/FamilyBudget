using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data.Entity;

namespace FamilyBudget.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Adding_Income_To_Db()
        {
            Context context = new Context();
            var incomeManager = new IncomeManager();
            AccountManager accountManager = new AccountManager();

            incomeManager.Context.Categories.Local.Add(new Category("З/п", "income"));
            incomeManager.Context.Currencies.Local.Add(new Currency("RUB"));
            Account textAccount = new Account("testAccount");
            accountManager.Context.Accounts.Local.Add(textAccount);
            accountManager.Context.SaveChanges();

            incomeManager.AddOperation(1000, DateTime.Parse("2014-02-02"),
                accountManager.Context.Accounts.Local.ElementAt(accountManager.Context.Accounts.Local.IndexOf(textAccount)), null,
                incomeManager.Context.Currencies.Local.ElementAt(0), incomeManager.Context.Categories.Local.ElementAt(0), "text comment");

            Operation addedOperation = context.Operations
            .OrderByDescending(op => op.Id)
            .First();

            Assert.ReferenceEquals(incomeManager.Context.Operations.Local.ElementAt(0), new Operation("income", 1000, DateTime.Parse("2014-02-02"),
              accountManager.Context.Accounts.Local.ElementAt(0), null,
            incomeManager.Context.Currencies.Local.ElementAt(0), incomeManager.Context.Categories.Local.ElementAt(0),
            "text comment") { Id = incomeManager.Context.Operations.Local.ElementAt(0).Id });
            
            
        }

        [TestMethod]
        public void Adding_Account_To_Db()
        {
            Context context = new Context();

            AccountManager accountManager = new AccountManager();
            context.Accounts.Local.Add(new Account("Сбербанк"));
            context.SaveChanges();
            Account addedAccount = context.Accounts
                .OrderByDescending(acc => acc.Id)
                .First();

            Assert.ReferenceEquals(addedAccount, new Account("Сбербанк")
            { Id = context.Accounts.Local.ElementAt(0).Id });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void If_Sum_Is_More_than_0()
        {
            Account account = new Account("Robert Brown");
            Category category = new Category("Food", "My dinner");
            Currency cur = new Currency("RUB");
            Operation operation = new Operation("income", -1000, DateTime.Parse("2014-06-13"), account, account, cur, category, "first part");
            
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Not_Adding_Simmiliar_Items()
        {
            Context context = new Context();
            var incomeManager = new IncomeManager();
            AccountManager accountManager = new AccountManager();

            incomeManager.Context.Categories.Local.Add(new Category("Sallary", "income"));
            incomeManager.Context.Currencies.Local.Add(new Currency("RUB"));
            Account textAccount = new Account("testAccount");
            accountManager.Context.Accounts.Local.Add(textAccount);
            accountManager.Context.SaveChanges();

            incomeManager.AddOperation(1000, DateTime.Parse("2014-02-02"),
                accountManager.Context.Accounts.Local.ElementAt(accountManager.Context.Accounts.Local.IndexOf(textAccount)),
                null, incomeManager.Context.Currencies.Local.ElementAt(0),
                incomeManager.Context.Categories.Local.ElementAt(0), "first part");
            incomeManager.AddOperation(1000, DateTime.Parse("2014-02-02"),
                accountManager.Context.Accounts.Local.ElementAt(accountManager.Context.Accounts.Local.IndexOf(textAccount)),
                null, incomeManager.Context.Currencies.Local.ElementAt(0),
                incomeManager.Context.Categories.Local.ElementAt(0), "first part");
            
        }
    }

}

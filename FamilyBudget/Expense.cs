using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget
{
    public class Expense : Operation
    {
        public int Id { get; set; }
        public Category Category { get; set; }

        public Expense(double sum, DateTime date, Account account, Currency currency, Category category)
            : base(sum, date, account, currency)
        {
            Category = category;
        }



        //public double Amount { get; set; }
        //List<Operation> operations;//все операции расходов

        //public void AddOperation(double sum, string category, DateTime data, string person, Account account) //добавляется операция в расходы и транзакции счета
        //{
        //    Operation operation = new Operation(sum, category, data, person, account);
        //    operations.Add(operation);
        //    account.AddTransaction(operation);
        //    account.Summary -= sum;
        //}

        //public void DeleteOperation(int index) //удаляется операция из расходов и транзакций счетов
        //{
        //    operations[index].Account.DeleteTransaction(operations[index]);
        //    operations[index].Account.Summary -= operations[index].Sum;
        //    operations.RemoveAt(index);
        //}

        //public void EditOperation(int index, double sum, string category, DateTime data, string person, Account account) //редактируется операция
        //{
        //    operations[index].Category = category;
        //    operations[index].Date = data;
        //    operations[index].Person = person;
        //    operations[index].Sum = sum;
        //    operations[index].Account = account;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget
{
    public class Transfer : Operation
    {
        public int Id { get; set; }
        public Account Account2 { get; set; }

        public Transfer(double sum, DateTime date, Account account, Currency currency, Account account2)
            : base(sum, date, account, currency)
        {
            Account2 = account2;
        }


        //public double Amount;
        //public void AddOperation(double sum, string category, DateTime data, string person);
        //public void DeleteOperation(int index);
        //public void EditOperation(int index, double sum, string category, DateTime data, string person);
    }
}

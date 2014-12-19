using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FamilyBudget
{
    interface IOperationManager
    {
        void InitializeData();
        void AddOperation(double sum, DateTime date, Account accountToPut, Account accountToWithdraw, Currency currency, Category category, string comment);
        void EditOperation(Operation operation, double sum, DateTime date, Account accountToPut, Account accountToWithdraw, Currency currency,
            Category category, string comment);
        void DeleteOperation(Operation operation);
    }
}

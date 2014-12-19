using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace FamilyBudget
{
    public class Operation : INotifyPropertyChanged
    {
        private int currencyId;
        private double sum;
        private int? accountToPutId, accountToWithdrawId, categoryId;
        private DateTime date;
        private string comment;
        public int Id { get; set; }
        public int? AccountToPutId
        {
            get
            {
                return accountToPutId;
            }
            set
            {
                accountToPutId = value;
            }
        }
        [NotMapped]
        public string AccountToPutToString
        {
            get
            {
                return AccountToPut.ToString();
            }
            set
            {
                AccountToPutToString = value;
            }
        }
        [NotMapped]
        public string AccountToWithdrawToString
        {
            get
            {
                return AccountToWithdraw.ToString();
            }
            set
            {
                AccountToWithdrawToString = value;
            }
        }

        [NotMapped]
        public string CurrencyToString
        {
            get
            {
                return Currency.ToString();
            }
            set
            {
                CurrencyToString = value;
            }
        }
        [NotMapped]
        public string CategoryToString
        {
            get
            {
                return Category.ToString();
            }
            set
            {
                CategoryToString = value;
            }
        }
        public int? AccountToWithdrawId
        {
            get
            {
                return accountToWithdrawId;
            }
            set
            {
                accountToWithdrawId = value;
            }
        }
        public int CurrencyId
        {
            get
            {
                return currencyId;
            }
            set
            {
                currencyId = value;
            }
        }
        public int? CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
            }
        }
        public string OperationType { get; set; }
        

        public double Sum
        {
            get
            {
                return sum;
            }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public Account AccountToPut { get; set; }
        public Account AccountToWithdraw { get; set; }
        public Currency Currency { get; set; }
        public Category Category { get; set; }

        [MaxLength(40)]
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public Operation(string operationType, double sum, DateTime date, Account accountToPut, Account accountToWithdraw, Currency currency, 
            Category category, string comment)
        {

            if (sum <= 0)
                throw new Exception(" Сумма должна быть положительным числом!");
            OperationType = operationType;
            Sum = sum;
            Date = date;

            if (accountToPut != null)
                AccountToPutId = accountToPut.Id;

            if (accountToWithdraw != null)
                AccountToWithdrawId = accountToWithdraw.Id;

            CurrencyId = currency.Id;

            if (category != null)
                CategoryId = category.Id;

            Comment = comment;
            
            
        }

        public Operation()
        {
        }
    }
}

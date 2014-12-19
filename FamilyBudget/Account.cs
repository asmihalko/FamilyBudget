using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FamilyBudget
{
    public class Account : INotifyPropertyChanged
    {
        private string name;
        private double rubSum, dolSum, euroSum;

        [MinLength(3)]
        [MaxLength(15)]
        public string Name 
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Id { get; set; }
        public double RubSum
        {
            get
            {
                return rubSum;
            }
            set
            {
                rubSum = value;
                OnPropertyChanged("RubSum");
            }
        }
        public double DolSum
        {
            get
            {
                return dolSum;
            }
            set
            {
                dolSum = value;
                OnPropertyChanged("DolSum");
            }
        }
        public double EuroSum
        {
            get
            {
                return euroSum;
            }
            set
            {
                euroSum = value;
                OnPropertyChanged("EuroSum");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            if (this != null)
                return Name;
            else
                return "";
        }
        public double Summary
        {
            get
            {
                return RubSum + DolSum + EuroSum;
            }
        }

        public Account(string name)
        {
            Name = name;
            RubSum = 0;
            DolSum = 0;
            EuroSum = 0;
        }

        public Account()
        {
        }
    }
}

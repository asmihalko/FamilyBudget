using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            if (this != null)
                return Name;
            else
                return "";
        }

        public Currency(string name)
        {
            Name = name;
        }

        public Currency()
        {
        }
    }
}

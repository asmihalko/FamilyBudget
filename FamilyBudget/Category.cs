using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryType { get; set; }

        public override string ToString()
        {
            if (this != null)
                return Name;
            else
                return "";
        }

        public Category(string name, string categoryType)
        {
            Name = name;
            CategoryType = categoryType;
        }

        public Category()
        {
        }
    }
}

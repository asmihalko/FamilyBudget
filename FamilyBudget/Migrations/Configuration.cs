namespace FamilyBudget.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<FamilyBudget.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "FamilyBudget.Context";
        }

        protected override void Seed(FamilyBudget.Context context)
        {
              //This method will be called after migrating to the latest version.

              //You can use the DbSet<T>.AddOrUpdate() helper extension method 
              //to avoid creating duplicate seed data. E.g.
            

            context.Currencies.AddOrUpdate(
           c => c.Name,
           new Currency { Name = "RUB"},
           new Currency { Name = "USD" },
           new Currency { Name = "EURO" }
         );

            context.Categories.AddOrUpdate(
           c => new { c.Name, c.CategoryType},
           new Category() { Name = "������ ������������", CategoryType = "income" },
              new Category() { Name = "���������", CategoryType = "income" },
              new Category() { Name = "�������", CategoryType = "income" },
              new Category() {  Name = "������������ ������", CategoryType = "income" },
              new Category() { Name = "����������", CategoryType = "income" },
              new Category() { Name = "�������", CategoryType = "income" },
              new Category() { Name = "������", CategoryType = "income" },
              new Category() { Name = "�������", CategoryType = "income" },
              new Category() { Name = "������� ���������", CategoryType = "income" },
              new Category() { Name = "���������", CategoryType = "income" },
              new Category() { Name = "�������", CategoryType = "income" },
              new Category() { Name = "����� �� ������������ ������������", CategoryType = "income" },
              new Category() { Name = "����������", CategoryType = "expense" },
              new Category() { Name = "��������", CategoryType = "expense" },
              new Category() { Name = "���������", CategoryType = "expense" },
              new Category() { Name = "������������ �������", CategoryType = "expense" },
              new Category() { Name = "�������", CategoryType = "expense" },
              new Category() { Name = "������", CategoryType = "expense" },
              new Category() { Name = "��������", CategoryType = "expense" },
              new Category() { Name = "�������", CategoryType = "expense" },
              new Category() { Name = "������", CategoryType = "expense" },
              new Category() { Name = "�������", CategoryType = "expense" },
              new Category() { Name = "�������� �������", CategoryType = "expense" },
              new Category() { Name = "�����������", CategoryType = "expense" },
              new Category() { Name = "������", CategoryType = "expense" },
              new Category() { Name = "�������", CategoryType = "expense" },
              new Category() { Name = "���������", CategoryType = "expense" },
              new Category() { Name = "������", CategoryType = "expense" },
              new Category() { Name = "������������� ������", CategoryType = "expense" } 
              );

            context.SaveChanges();
            
        }
    }
}

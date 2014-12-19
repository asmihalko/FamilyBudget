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
           new Category() { Name = "Аренда недвижимости", CategoryType = "income" },
              new Category() { Name = "Дивиденды", CategoryType = "income" },
              new Category() { Name = "Лотерея", CategoryType = "income" },
              new Category() {  Name = "Материальная помощь", CategoryType = "income" },
              new Category() { Name = "Наследство", CategoryType = "income" },
              new Category() { Name = "Находка", CategoryType = "income" },
              new Category() { Name = "Пенсия", CategoryType = "income" },
              new Category() { Name = "Подарок", CategoryType = "income" },
              new Category() { Name = "Продажа имущества", CategoryType = "income" },
              new Category() { Name = "Стипендия", CategoryType = "income" },
              new Category() { Name = "Халтура", CategoryType = "income" },
              new Category() { Name = "Доход от коммерческой деятельности", CategoryType = "income" },
              new Category() { Name = "Автомобиль", CategoryType = "expense" },
              new Category() { Name = "Животные", CategoryType = "expense" },
              new Category() { Name = "Коммиссия", CategoryType = "expense" },
              new Category() { Name = "Коммунальные платежи", CategoryType = "expense" },
              new Category() { Name = "Кредиты", CategoryType = "expense" },
              new Category() { Name = "Мебель", CategoryType = "expense" },
              new Category() { Name = "Медицины", CategoryType = "expense" },
              new Category() { Name = "Общепит", CategoryType = "expense" },
              new Category() { Name = "Одежда", CategoryType = "expense" },
              new Category() { Name = "Подарки", CategoryType = "expense" },
              new Category() { Name = "Продукты питания", CategoryType = "expense" },
              new Category() { Name = "Развлечения", CategoryType = "expense" },
              new Category() { Name = "Ремонт", CategoryType = "expense" },
              new Category() { Name = "Техника", CategoryType = "expense" },
              new Category() { Name = "Транспорт", CategoryType = "expense" },
              new Category() { Name = "Услуги", CategoryType = "expense" },
              new Category() { Name = "Хозяйственные товары", CategoryType = "expense" } 
              );

            context.SaveChanges();
            
        }
    }
}

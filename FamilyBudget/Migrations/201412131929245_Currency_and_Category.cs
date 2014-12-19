namespace FamilyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Currency_and_Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Expenses", "Category_Id", c => c.Int());
            AddColumn("dbo.Expenses", "Currency_Id", c => c.Int());
            AddColumn("dbo.Incomes", "Category_Id", c => c.Int());
            AddColumn("dbo.Incomes", "Currency_Id", c => c.Int());
            AddColumn("dbo.Transfers", "Currency_Id", c => c.Int());
            CreateIndex("dbo.Expenses", "Category_Id");
            CreateIndex("dbo.Expenses", "Currency_Id");
            CreateIndex("dbo.Incomes", "Category_Id");
            CreateIndex("dbo.Incomes", "Currency_Id");
            CreateIndex("dbo.Transfers", "Currency_Id");
            AddForeignKey("dbo.Expenses", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Expenses", "Currency_Id", "dbo.Currencies", "Id");
            AddForeignKey("dbo.Incomes", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Incomes", "Currency_Id", "dbo.Currencies", "Id");
            AddForeignKey("dbo.Transfers", "Currency_Id", "dbo.Currencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transfers", "Currency_Id", "dbo.Currencies");
            DropForeignKey("dbo.Incomes", "Currency_Id", "dbo.Currencies");
            DropForeignKey("dbo.Incomes", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Expenses", "Currency_Id", "dbo.Currencies");
            DropForeignKey("dbo.Expenses", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Transfers", new[] { "Currency_Id" });
            DropIndex("dbo.Incomes", new[] { "Currency_Id" });
            DropIndex("dbo.Incomes", new[] { "Category_Id" });
            DropIndex("dbo.Expenses", new[] { "Currency_Id" });
            DropIndex("dbo.Expenses", new[] { "Category_Id" });
            DropColumn("dbo.Transfers", "Currency_Id");
            DropColumn("dbo.Incomes", "Currency_Id");
            DropColumn("dbo.Incomes", "Category_Id");
            DropColumn("dbo.Expenses", "Currency_Id");
            DropColumn("dbo.Expenses", "Category_Id");
            DropTable("dbo.Currencies");
            DropTable("dbo.Categories");
        }
    }
}

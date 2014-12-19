namespace FamilyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
            
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        AccountToPut_Id = c.Int(),
                        AccountToWithdraw_Id = c.Int(),
                        Category_Id = c.Int(),
                        Currency_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountToPut_Id)
                .ForeignKey("dbo.Accounts", t => t.AccountToWithdraw_Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id)
                .Index(t => t.AccountToPut_Id)
                .Index(t => t.AccountToWithdraw_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Currency_Id);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RubSum = c.Double(nullable: false),
                        DolSum = c.Double(nullable: false),
                        EuroSum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Operations", "Currency_Id", "dbo.Currencies");
            DropForeignKey("dbo.Operations", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Operations", "AccountToWithdraw_Id", "dbo.Accounts");
            DropForeignKey("dbo.Operations", "AccountToPut_Id", "dbo.Accounts");
            DropIndex("dbo.Operations", new[] { "Currency_Id" });
            DropIndex("dbo.Operations", new[] { "Category_Id" });
            DropIndex("dbo.Operations", new[] { "AccountToWithdraw_Id" });
            DropIndex("dbo.Operations", new[] { "AccountToPut_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
            DropTable("dbo.Operations");
            DropTable("dbo.Currencies");
            DropTable("dbo.Categories");
        }
    }
}

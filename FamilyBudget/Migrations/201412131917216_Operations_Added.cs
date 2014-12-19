namespace FamilyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Operations_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
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
                "dbo.Incomes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Transfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                        Account2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Accounts", t => t.Account2_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Account2_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transfers", "Account2_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transfers", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Incomes", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Expenses", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Transfers", new[] { "Account2_Id" });
            DropIndex("dbo.Transfers", new[] { "Account_Id" });
            DropIndex("dbo.Incomes", new[] { "Account_Id" });
            DropIndex("dbo.Expenses", new[] { "Account_Id" });
            DropTable("dbo.Transfers");
            DropTable("dbo.Incomes");
            DropTable("dbo.Accounts");
            DropTable("dbo.Expenses");
        }
    }
}

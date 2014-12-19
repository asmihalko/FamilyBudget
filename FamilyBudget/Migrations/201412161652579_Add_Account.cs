namespace FamilyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Account : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryType", c => c.String());
            AddColumn("dbo.Operations", "OperationType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Operations", "OperationType");
            DropColumn("dbo.Categories", "CategoryType");
        }
    }
}

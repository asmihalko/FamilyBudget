namespace FamilyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Name", c => c.String(maxLength: 15));
            AlterColumn("dbo.Operations", "Comment", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Operations", "Comment", c => c.String());
            AlterColumn("dbo.Accounts", "Name", c => c.String());
        }
    }
}

namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_ispaid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.cashouts", "IsPaid", c => c.Boolean(nullable: false));
            AlterColumn("dbo.cashouts", "Apply", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.cashouts", "Apply", c => c.DateTime(nullable: false));
            AlterColumn("dbo.cashouts", "IsPaid", c => c.Boolean(nullable: false));
        }
    }
}

namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_datetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.cashouts", "Paid", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.cashouts", "Paid", c => c.DateTime(nullable: false));
        }
    }
}

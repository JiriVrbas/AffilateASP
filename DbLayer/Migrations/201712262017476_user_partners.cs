namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_partners : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.user_partners", "LinkToAffilate", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.user_partners", "LinkToAffilate", c => c.String());
        }
    }
}

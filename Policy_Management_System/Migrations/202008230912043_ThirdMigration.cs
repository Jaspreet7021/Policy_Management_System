namespace Policy_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logins", "Mobile", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logins", "Mobile");
        }
    }
}

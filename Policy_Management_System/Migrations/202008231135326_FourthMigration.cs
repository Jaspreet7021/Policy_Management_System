namespace Policy_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Registrations", "Mobile", c => c.String(nullable: false));
            AlterColumn("dbo.Logins", "Mobile", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logins", "Mobile", c => c.Int(nullable: false));
            AlterColumn("dbo.Registrations", "Mobile", c => c.Int(nullable: false));
        }
    }
}

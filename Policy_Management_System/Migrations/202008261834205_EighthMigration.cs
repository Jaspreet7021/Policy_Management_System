namespace Policy_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EighthMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PolicyDetails", "Premium", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PolicyDetails", "Premium", c => c.Double(nullable: false));
        }
    }
}

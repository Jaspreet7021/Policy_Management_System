namespace Policy_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logins", "Role_Id", "dbo.Roles");
            DropIndex("dbo.Logins", new[] { "Role_Id" });
            DropColumn("dbo.Logins", "Role_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logins", "Role_Id", c => c.Int());
            CreateIndex("dbo.Logins", "Role_Id");
            AddForeignKey("dbo.Logins", "Role_Id", "dbo.Roles", "Id");
        }
    }
}

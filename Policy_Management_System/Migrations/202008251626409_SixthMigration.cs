namespace Policy_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SixthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PolicyDetails", "ProposerId_Id", c => c.Int());
            CreateIndex("dbo.PolicyDetails", "ProposerId_Id");
            AddForeignKey("dbo.PolicyDetails", "ProposerId_Id", "dbo.Registrations", "Id");
            DropColumn("dbo.Registrations", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Registrations", "Email", c => c.String(nullable: false));
            DropForeignKey("dbo.PolicyDetails", "ProposerId_Id", "dbo.Registrations");
            DropIndex("dbo.PolicyDetails", new[] { "ProposerId_Id" });
            DropColumn("dbo.PolicyDetails", "ProposerId_Id");
        }
    }
}

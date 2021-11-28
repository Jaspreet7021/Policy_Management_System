namespace Policy_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeventhMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PolicyDetails", "SubCatId_Id", "dbo.SubCategories");
            DropIndex("dbo.PolicyDetails", new[] { "SubCatId_Id" });
            DropColumn("dbo.PolicyDetails", "SubCatId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PolicyDetails", "SubCatId_Id", c => c.Int());
            CreateIndex("dbo.PolicyDetails", "SubCatId_Id");
            AddForeignKey("dbo.PolicyDetails", "SubCatId_Id", "dbo.SubCategories", "Id");
        }
    }
}

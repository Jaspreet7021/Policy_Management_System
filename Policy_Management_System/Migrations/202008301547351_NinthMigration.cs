namespace Policy_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LifeInsaurances", "CustomerId_Id", "dbo.Registrations");
            DropForeignKey("dbo.Nominees", "CustomerId_Id", "dbo.Registrations");
            DropForeignKey("dbo.LifeInsaurances", "NomineeId_Id", "dbo.Nominees");
            DropForeignKey("dbo.LifeInsaurances", "PolicyId_Id", "dbo.PolicyDetails");
            DropForeignKey("dbo.SubCategories", "CatId_Id", "dbo.Categories");
            DropForeignKey("dbo.VehicleInsaurances", "CustomerId_Id", "dbo.Registrations");
            DropIndex("dbo.LifeInsaurances", new[] { "CustomerId_Id" });
            DropIndex("dbo.LifeInsaurances", new[] { "NomineeId_Id" });
            DropIndex("dbo.LifeInsaurances", new[] { "PolicyId_Id" });
            DropIndex("dbo.Nominees", new[] { "CustomerId_Id" });
            DropIndex("dbo.SubCategories", new[] { "CatId_Id" });
            DropIndex("dbo.VehicleInsaurances", new[] { "CustomerId_Id" });
            DropTable("dbo.LifeInsaurances");
            DropTable("dbo.Nominees");
            DropTable("dbo.SubCategories");
            DropTable("dbo.VehicleInsaurances");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VehicleInsaurances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleType = c.String(nullable: false),
                        BrandType = c.String(nullable: false),
                        FuelType = c.String(nullable: false),
                        RegistrationYear = c.DateTime(nullable: false),
                        VehicleNumber = c.String(),
                        CustomerId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CatId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Nominees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomineeName = c.String(),
                        Relation = c.String(),
                        Gender = c.String(),
                        AllocatedPercentage = c.Double(nullable: false),
                        Address = c.String(),
                        CustomerId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LifeInsaurances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId_Id = c.Int(),
                        NomineeId_Id = c.Int(),
                        PolicyId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.VehicleInsaurances", "CustomerId_Id");
            CreateIndex("dbo.SubCategories", "CatId_Id");
            CreateIndex("dbo.Nominees", "CustomerId_Id");
            CreateIndex("dbo.LifeInsaurances", "PolicyId_Id");
            CreateIndex("dbo.LifeInsaurances", "NomineeId_Id");
            CreateIndex("dbo.LifeInsaurances", "CustomerId_Id");
            AddForeignKey("dbo.VehicleInsaurances", "CustomerId_Id", "dbo.Registrations", "Id");
            AddForeignKey("dbo.SubCategories", "CatId_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.LifeInsaurances", "PolicyId_Id", "dbo.PolicyDetails", "Id");
            AddForeignKey("dbo.LifeInsaurances", "NomineeId_Id", "dbo.Nominees", "Id");
            AddForeignKey("dbo.Nominees", "CustomerId_Id", "dbo.Registrations", "Id");
            AddForeignKey("dbo.LifeInsaurances", "CustomerId_Id", "dbo.Registrations", "Id");
        }
    }
}

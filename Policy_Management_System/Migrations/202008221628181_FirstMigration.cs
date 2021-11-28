namespace Policy_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registrations", t => t.CustomerId_Id)
                .ForeignKey("dbo.Nominees", t => t.NomineeId_Id)
                .ForeignKey("dbo.PolicyDetails", t => t.PolicyId_Id)
                .Index(t => t.CustomerId_Id)
                .Index(t => t.NomineeId_Id)
                .Index(t => t.PolicyId_Id);
            
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Mobile = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        State = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        Password = c.String(nullable: false),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registrations", t => t.CustomerId_Id)
                .Index(t => t.CustomerId_Id);
            
            CreateTable(
                "dbo.PolicyDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PolicyName = c.String(nullable: false),
                        SumAssured = c.Double(nullable: false),
                        Premium = c.Double(nullable: false),
                        Tenure = c.Int(nullable: false),
                        PolicyInfo = c.String(nullable: false),
                        CatId_Id = c.Int(),
                        SubCatId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CatId_Id)
                .ForeignKey("dbo.SubCategories", t => t.SubCatId_Id)
                .Index(t => t.CatId_Id)
                .Index(t => t.SubCatId_Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CatId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CatId_Id)
                .Index(t => t.CatId_Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        Role_Id = c.Int(),
                        UserId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.Registrations", t => t.UserId_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.UserId_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registrations", t => t.CustomerId_Id)
                .Index(t => t.CustomerId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleInsaurances", "CustomerId_Id", "dbo.Registrations");
            DropForeignKey("dbo.Logins", "UserId_Id", "dbo.Registrations");
            DropForeignKey("dbo.Logins", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.LifeInsaurances", "PolicyId_Id", "dbo.PolicyDetails");
            DropForeignKey("dbo.PolicyDetails", "SubCatId_Id", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CatId_Id", "dbo.Categories");
            DropForeignKey("dbo.PolicyDetails", "CatId_Id", "dbo.Categories");
            DropForeignKey("dbo.LifeInsaurances", "NomineeId_Id", "dbo.Nominees");
            DropForeignKey("dbo.Nominees", "CustomerId_Id", "dbo.Registrations");
            DropForeignKey("dbo.LifeInsaurances", "CustomerId_Id", "dbo.Registrations");
            DropForeignKey("dbo.Registrations", "Role_Id", "dbo.Roles");
            DropIndex("dbo.VehicleInsaurances", new[] { "CustomerId_Id" });
            DropIndex("dbo.Logins", new[] { "UserId_Id" });
            DropIndex("dbo.Logins", new[] { "Role_Id" });
            DropIndex("dbo.SubCategories", new[] { "CatId_Id" });
            DropIndex("dbo.PolicyDetails", new[] { "SubCatId_Id" });
            DropIndex("dbo.PolicyDetails", new[] { "CatId_Id" });
            DropIndex("dbo.Nominees", new[] { "CustomerId_Id" });
            DropIndex("dbo.Registrations", new[] { "Role_Id" });
            DropIndex("dbo.LifeInsaurances", new[] { "PolicyId_Id" });
            DropIndex("dbo.LifeInsaurances", new[] { "NomineeId_Id" });
            DropIndex("dbo.LifeInsaurances", new[] { "CustomerId_Id" });
            DropTable("dbo.VehicleInsaurances");
            DropTable("dbo.Logins");
            DropTable("dbo.SubCategories");
            DropTable("dbo.PolicyDetails");
            DropTable("dbo.Nominees");
            DropTable("dbo.Roles");
            DropTable("dbo.Registrations");
            DropTable("dbo.LifeInsaurances");
            DropTable("dbo.Categories");
        }
    }
}

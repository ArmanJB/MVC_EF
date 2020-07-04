namespace MVC_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        ContactID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.Contact", t => t.ContactID, cascadeDelete: true)
                .Index(t => t.ContactID);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactID = c.Int(nullable: false, identity: true),
                        AreaID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContactID)
                .ForeignKey("dbo.Area", t => t.AreaID, cascadeDelete: true)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        AreaID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AreaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "AreaID", "dbo.Area");
            DropForeignKey("dbo.Address", "ContactID", "dbo.Contact");
            DropIndex("dbo.Contact", new[] { "AreaID" });
            DropIndex("dbo.Address", new[] { "ContactID" });
            DropTable("dbo.Area");
            DropTable("dbo.Contact");
            DropTable("dbo.Address");
        }
    }
}

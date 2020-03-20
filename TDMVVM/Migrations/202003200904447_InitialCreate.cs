namespace TDMVVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Amis",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false, precision: 0),
                        telperso = c.String(unicode: false),
                        nom = c.String(nullable: false, maxLength: 80, storeType: "nvarchar"),
                        prenom = c.String(nullable: false, maxLength: 80, storeType: "nvarchar"),
                        email = c.String(maxLength: 160, storeType: "nvarchar"),
                        adresse = c.String(maxLength: 255, storeType: "nvarchar"),
                        telephone = c.String(maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        noclient = c.Int(nullable: false),
                        guid = c.String(maxLength: 80, storeType: "nvarchar"),
                        date = c.DateTime(nullable: false, precision: 0),
                        nom = c.String(nullable: false, maxLength: 80, storeType: "nvarchar"),
                        prenom = c.String(nullable: false, maxLength: 80, storeType: "nvarchar"),
                        email = c.String(maxLength: 160, storeType: "nvarchar"),
                        adresse = c.String(maxLength: 255, storeType: "nvarchar"),
                        telephone = c.String(maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clients");
            DropTable("dbo.Amis");
        }
    }
}

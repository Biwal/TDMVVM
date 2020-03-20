namespace TDMVVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Amis", "telperso", c => c.String(maxLength: 20, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Amis", "telperso", c => c.String(unicode: false));
        }
    }
}

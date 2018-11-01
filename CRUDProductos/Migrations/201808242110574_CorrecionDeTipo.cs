namespace CRUDProductos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecionDeTipo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productoes", "Precio", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productoes", "Precio", c => c.Double(nullable: false));
        }
    }
}

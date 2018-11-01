namespace CRUDProductos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecionDeTipo2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productoes", "Precio", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productoes", "Precio", c => c.Single(nullable: false));
        }
    }
}

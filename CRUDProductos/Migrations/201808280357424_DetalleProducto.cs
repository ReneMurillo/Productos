namespace CRUDProductos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetalleProducto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "Existencias", c => c.Int(nullable: false));
            AddColumn("dbo.Productoes", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Productoes", "Cantidad", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productoes", "Cantidad", c => c.Int(nullable: false));
            DropColumn("dbo.Productoes", "Discriminator");
            DropColumn("dbo.Productoes", "Existencias");
        }
    }
}

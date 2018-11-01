namespace CRUDProductos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validaciones : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productoes", "Nombre", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Productoes", "Descripcion", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productoes", "Descripcion", c => c.String());
            AlterColumn("dbo.Productoes", "Nombre", c => c.String());
        }
    }
}

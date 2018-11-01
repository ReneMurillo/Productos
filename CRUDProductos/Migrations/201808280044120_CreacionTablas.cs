namespace CRUDProductos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionTablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetallePedidoes",
                c => new
                    {
                        DetallePedidoId = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 30),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DetallePedidoId)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        EstadoPedido = c.Int(nullable: false),
                        FechaOrden = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PedidoId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombres = c.String(nullable: false, maxLength: 30),
                        Apellidos = c.String(nullable: false, maxLength: 30),
                        Dui = c.String(nullable: false, maxLength: 10),
                        FechaNacimiento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetallePedidoes", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.DetallePedidoes", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.Pedidoes", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.Pedidoes", new[] { "ClienteId" });
            DropIndex("dbo.DetallePedidoes", new[] { "ProductoId" });
            DropIndex("dbo.DetallePedidoes", new[] { "PedidoId" });
            DropTable("dbo.Clientes");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.DetallePedidoes");
        }
    }
}

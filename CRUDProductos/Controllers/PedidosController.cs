using CRUDProductos.Context;
using CRUDProductos.Models;
using CRUDProductos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDProductos.Controllers
{
    public class PedidosController : Controller
    {
        ProductContext db = new ProductContext();
        // GET: Pedidos
        public ActionResult NuevoPedido()
        {
            var vistaPedido = new VistaPedido();
            var list = db.Clientes.ToList();
            list.Add(new Cliente { ClienteId = 0, Nombres = "[Seleccione un cliente...]" });
            list = list.OrderBy(c => c.NombreCompleto).ToList();
            ViewBag.ClienteId = new SelectList(list, "ClienteId", "NombreCompleto");
            Session["orderView"] = vistaPedido;

            
            vistaPedido.Cliente = new Cliente();
            vistaPedido.Productos = new List<PedidoProducto>();
            return View(vistaPedido);
        }

        [HttpPost]
        public ActionResult NuevoPedido(VistaPedido vistaPedido)
        {
            vistaPedido = Session["orderView"] as VistaPedido;

            var clienteId = int.Parse(Request["ClienteId"]);

            if (clienteId == 0)
            {
                var listC = db.Clientes.ToList();
                listC.Add(new Cliente { ClienteId = 0, Nombres = "[Seleccione un cliente...]" });
                listC = listC.OrderBy(c => c.NombreCompleto).ToList();
                ViewBag.ClienteId = new SelectList(listC, "ClienteId", "NombreCompleto");
                ViewBag.Error = "Debe seleccionar un cliente";
                return View(vistaPedido);
            }

            var customer = db.Clientes.Find(clienteId);

            if (customer == null)
            {
                var listC = db.Clientes.ToList();
                listC.Add(new Cliente { ClienteId = 0, Nombres = "[Seleccione un cliente...]" });
                listC = listC.OrderBy(c => c.NombreCompleto).ToList();
                ViewBag.ClienteId = new SelectList(listC, "ClienteId", "NombreCompleto");
                ViewBag.Error = "Debe seleccionar un cliente";
                return View(vistaPedido);
            }

            if (vistaPedido.Productos.Count == 0)
            {
                var listC = db.Clientes.ToList();
                listC.Add(new Cliente { ClienteId = 0, Nombres = "[Seleccione un cliente...]" });
                listC = listC.OrderBy(c => c.NombreCompleto).ToList();
                ViewBag.ClienteId = new SelectList(listC, "ClienteId", "NombreCompleto");
                ViewBag.Error = "Debe seleccionar un cliente";
                return View(vistaPedido);
            }

            int pedidoId = 0;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var pedido = new Pedido
                    {
                        ClienteId = clienteId,
                        FechaOrden = DateTime.Now,
                        EstadoPedido = EstadoPedido.Creado,
                    };

                    db.Pedidos.Add(pedido);
                    db.SaveChanges();


                    pedidoId = db.Pedidos.ToList().Select(o => o.PedidoId).Max();

                    foreach (var item in vistaPedido.Productos)
                    {
                        var detallePedido = new DetallePedido
                        {
                            ProductoId = item.ProductoId,
                            
                            Descripcion = item.Descripcion,
                            Precio = item.Precio,
                            Cantidad = item.Cantidad,
                            PedidoId = pedidoId
                        };
                        db.DetallePedidos.Add(detallePedido);
                        db.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var listC = db.Clientes.ToList();
                    listC.Add(new Cliente { ClienteId = 0, Nombres = "[Seleccione un cliente...]" });
                    listC = listC.OrderBy(c => c.NombreCompleto).ToList();
                    ViewBag.ClienteId = new SelectList(listC, "ClienteId", "NombreCompleto");
                    ViewBag.Error = "ERROR: " + ex.Message;
                    return View(vistaPedido);
                }


            }
            var listCu = db.Clientes.ToList();
            listCu.Add(new Cliente { ClienteId = 0, Nombres = "[Seleccione un cliente...]" });
            listCu = listCu.OrderBy(c => c.NombreCompleto).ToList();
            ViewBag.ClienteId = new SelectList(listCu, "ClienteId", "NombreCompleto");
            ViewBag.Message = string.Format("La orden: {0}, registrada exitosamente", pedidoId);
            

            vistaPedido = new VistaPedido();
            vistaPedido.Cliente = new Cliente();
            vistaPedido.Productos = new List<PedidoProducto>();
            Session["orderView"] = vistaPedido;

            return View(vistaPedido);

        }

        public ActionResult AgregarProducto()
        {
            var list = db.Productos.ToList();
            list.Add(new PedidoProducto { ProductoId = 0, Descripcion = "[Seleccione un producto...]" });
            list = list.OrderBy(p => p.Nombre).ToList();
            ViewBag.ProductoId = new SelectList(list, "ProductoId", "Nombre");

            return View();
        }

        [HttpPost]
        public ActionResult AgregarProducto(PedidoProducto pedidoProducto)
        {
            var vistPedido = Session["orderView"] as VistaPedido;

            var productoId = int.Parse(Request["ProductoId"]);

            if (productoId == 0)
            {
                var list = db.Productos.ToList();
                list.Add(new PedidoProducto { ProductoId = 0, Descripcion = "[Seleccione un producto...]" });
                list = list.OrderBy(p => p.Nombre).ToList();
                ViewBag.ProductoId = new SelectList(list, "ProductoId", "Nombre");
                ViewBag.Error = "Debe seleccionar un producto";
                return View(pedidoProducto);
            }

            var producto = db.Productos.Find(productoId);

            if (producto == null)
            {
                var list = db.Productos.ToList();
                list.Add(new PedidoProducto { ProductoId = 0, Nombre = "[Seleccione un producto...]" });
                list = list.OrderBy(p => p.Nombre).ToList();
                ViewBag.ProductoId = new SelectList(list, "ProductoId", "Nombre");
                ViewBag.Error = "El producto no existe";
                return View(pedidoProducto);
            }

            pedidoProducto = vistPedido.Productos.Find(p => p.ProductoId == productoId);

            if (pedidoProducto == null)
            {
                pedidoProducto = new PedidoProducto
                {
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    ProductoId = producto.ProductoId,
                    Cantidad = int.Parse(Request["Cantidad"])
                };
                vistPedido.Productos.Add(pedidoProducto);
            }
            else
            {
                pedidoProducto.Cantidad += int.Parse(Request["Cantidad"]);
            }


            var listC = db.Clientes.ToList();
            listC.Add(new Cliente { ClienteId = 0, Nombres = "[Seleccione un cliente...]" });
            listC = listC.OrderBy(c => c.NombreCompleto).ToList();
            ViewBag.ClienteId = new SelectList(listC, "ClienteId", "NombreCompleto");

            return View("NuevoPedido", vistPedido);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
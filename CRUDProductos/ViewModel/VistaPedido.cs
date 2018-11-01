using CRUDProductos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDProductos.ViewModel
{
    public class VistaPedido
    {
        public Cliente Cliente { get; set; }

        public PedidoProducto Producto { get; set; }

        public List<PedidoProducto> Productos { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUDProductos.Models
{
    public class PedidoProducto: Producto
    {
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int Cantidad { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Total { get { return Precio * (decimal)Cantidad; } }
    }
}
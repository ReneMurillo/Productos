using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUDProductos.Models
{
    public class DetallePedido
    {
        [Key]
        public int DetallePedidoId { get; set; }

        public int PedidoId { get; set; }

        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Display(Name = "Descripción")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {1} y {2} caracteres", MinimumLength = 3)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar el {0}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Precio { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public virtual Pedido Pedido { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
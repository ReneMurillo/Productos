using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUDProductos.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string  Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        public int Existencias { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        public string Descripcion { get; set; }

        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
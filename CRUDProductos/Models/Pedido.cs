using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUDProductos.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }

        public int ClienteId { get; set; }

        public EstadoPedido EstadoPedido { get; set; }

        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaOrden { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
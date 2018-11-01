using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUDProductos.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "El campo {0} debe contener {2} caracteres", MinimumLength = 10)]
        public string Dui { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        public string NombreCompleto { get { return string.Format("{0} {1}", Nombres, Apellidos); } }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
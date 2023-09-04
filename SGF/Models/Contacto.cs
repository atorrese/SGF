using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SGF.Models
{
    public class Contacto
    {
        [Key]
        public int IdContacto { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
    }
}

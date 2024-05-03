using NPOI.SS.Formula.Functions;
using System.ComponentModel.DataAnnotations;

namespace SGF.Models
{
    public class Presupuesto
    {
        [Key]
        public long Id { get; set; }
        public string Cliente { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public double Monto { get; set; }
        public double? Esigef { get; set; }
        public double? EnEsigef { get; set; } 

    }
}

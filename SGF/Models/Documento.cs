using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SGF.Models;

public class Documento
{
    [Key]
    public long Id { get; set; }
    public string Ruc { get; set; }
    public string Emisor { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public string DocXml { get; set; }
    public string? DocPdf { get; set; }
}
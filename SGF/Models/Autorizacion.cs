using Newtonsoft.Json.Linq;

namespace SGF.Models;

public class Autorizacion
{
    public string estado { get; set; }
    public string numeroAutorizacion { get; set; }
    public string fechaAutorizacion { get; set; }
    public string ambiente { get; set; }
    public string comprobante { get; set; }
    public string? mensaje { get; set; }
    
}
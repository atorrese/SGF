using Newtonsoft.Json;

namespace SGF.Models;

public class FlowPedido
{
    [JsonProperty("id")]
    public long Id { get; set; }
    
    [JsonProperty("numero_pedido")]
    public string NumeroPedido { get; set; }
    
    [JsonProperty("titulo")]
    public string Titulo { get; set; }
    
    public string Proveedor { get; set; }
    
    [JsonProperty("fecha")]
    public DateTime Fecha { get; set; } = DateTime.Now;
    
    [JsonProperty("fecha_almacen")]
    public DateTime FechaAlmacen { get; set; }
    
    [JsonProperty("fecha_disponible")]
    public DateTime FechaDisponible { get; set; }
    
    [JsonProperty("urgente")]
    public bool Urgente { get; set; }
    
    [JsonProperty("total")]
    public double Total { get; set; }
    
    [JsonProperty("estado")]
    public double Estado { get; set; }
    
    [JsonProperty("cantidad_aprobaciones")]
    public double CantidadAprobaciones { get; set; }
    
    [JsonProperty("texto_cantidad_aprobaciones")]
    public string TextoCantidadAprobaciones { get; set; }
    
    [JsonProperty("detalle")]
    public List<FlowPedidoDetalle>? Detalles { get; set; }
    
    
}
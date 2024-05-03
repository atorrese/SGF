namespace SGF.Models;

public class FlowPedidoDetalle
{
    public int Id { get; set; }
    public int PedidoId { get; set; }  // Clave foránea a FlowPedido
    public string Producto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
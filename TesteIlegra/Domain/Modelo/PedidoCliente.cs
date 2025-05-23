namespace TesteIlegra.Domain.Modelo
{
    public class PedidoCliente
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public List<ItemPedido> Itens { get; set; } = new();
    }
}

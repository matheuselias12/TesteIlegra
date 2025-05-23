using System.Diagnostics.CodeAnalysis;

namespace TesteIlegra.Domain.Modelo
{
    [ExcludeFromCodeCoverage]
    public class PedidoCliente
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public List<ItemPedido> Itens { get; set; } = new();
    }
}

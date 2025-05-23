using System.Diagnostics.CodeAnalysis;

namespace TesteIlegra.Domain.Modelo
{
    [ExcludeFromCodeCoverage]
    public class PedidoRevenda
    {
        public Guid Id { get; set; }
        public Guid RevendaId { get; set; }
        public List<ItemPedido> Itens { get; set; } = new();
    }
}

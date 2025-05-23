using System.Diagnostics.CodeAnalysis;

namespace TesteIlegra.Domain.Modelo
{
    [ExcludeFromCodeCoverage]
    public class ItemPedido
    {
        public string Produto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
}

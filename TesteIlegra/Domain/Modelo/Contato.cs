using System.Diagnostics.CodeAnalysis;

namespace TesteIlegra.Domain.Modelo
{
    [ExcludeFromCodeCoverage]
    public class Contato
    {
        public string Nome { get; set; } = string.Empty;
        public bool Principal { get; set; }
    }
}

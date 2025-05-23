using System.Diagnostics.CodeAnalysis;

namespace TesteIlegra.Domain.Modelo
{
    [ExcludeFromCodeCoverage]
    public class Endereco
    {
        public string Rua { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
    }
}

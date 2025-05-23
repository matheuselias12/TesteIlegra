using System.Diagnostics.CodeAnalysis;

namespace TesteIlegra.Domain.Modelo
{
    [ExcludeFromCodeCoverage]
    public class Revenda
    {
        public Guid RevendaId { get; set; }
        public string Cnpj { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Telefone> Telefones { get; set; } = new();
        public List<Contato> Contatos { get; set; } = new();
        public List<Endereco> EnderecosEntrega { get; set; } = new();
    }
}

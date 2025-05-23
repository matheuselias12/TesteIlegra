using FluentValidation;
using TesteIlegra.Domain.Modelo;

namespace TesteIlegra.API.Config
{
    public class RevendaValidator : AbstractValidator<Revenda>
    {
        public RevendaValidator()
        {
            RuleFor(r => r.Cnpj).NotEmpty().WithMessage("CNPJ é obrigatório")
                                .Must(ValidarCnpj).WithMessage("CNPJ inválido");

            RuleFor(r => r.RazaoSocial).NotEmpty().WithMessage("Razão Social é obrigatória");

            RuleFor(r => r.NomeFantasia).NotEmpty().WithMessage("Nome Fantasia é obrigatório");

            RuleFor(r => r.Email).NotEmpty().EmailAddress().WithMessage("Email inválido");

            RuleFor(r => r.Contatos).NotEmpty().WithMessage("É necessário pelo menos um contato")
                .Must(c => c.Any(x => x.Principal)).WithMessage("Deve haver um contato principal");

            RuleFor(r => r.EnderecosEntrega).NotEmpty().WithMessage("É necessário pelo menos um endereço de entrega");
        }

        private bool ValidarCnpj(string cnpj)
        {
            return !string.IsNullOrWhiteSpace(cnpj) && cnpj.Length >= 14;
        }
    }
}

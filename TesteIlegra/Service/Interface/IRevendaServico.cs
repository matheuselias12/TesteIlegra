using TesteIlegra.Domain.Modelo;

namespace TesteIlegra.Service.Interface
{
    public interface IRevendaServico
    {
        Task<Guid> CadastrarRevendaAsync(Revenda revenda);
        Task<Guid> ReceberPedidoClienteAsync(PedidoCliente pedido);
        Task<bool> EmitirPedidoParaVebmaAsync(Guid revendaId, Guid pedidoId);
    }
}

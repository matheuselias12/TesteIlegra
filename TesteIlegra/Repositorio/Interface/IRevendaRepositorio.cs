using TesteIlegra.Domain.Modelo;

namespace TesteIlegra.Repositorio.Interface
{
    public interface IRevendaRepositorio
    {
        Task<Guid> AdicionarAsync(Revenda revenda);
        Task<Guid> AdicionarAsync(PedidoCliente pedidoCliente);
        Task<Revenda?> ObterPorIdAsync(Guid id);
        Task<PedidoCliente> ObterPedidoPorIdAsync(Guid pedidoId);
    }
}

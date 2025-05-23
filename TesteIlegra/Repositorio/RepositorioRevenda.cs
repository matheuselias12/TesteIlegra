using TesteIlegra.Domain.Modelo;

namespace TesteIlegra.Repositorio
{
    public class RepositorioRevenda
    {
        private readonly List<Revenda> _revendas = new();
        private readonly List<PedidoCliente> _pedidosClientes = new();

        public Guid AdicionarRevenda(Revenda revenda)
        {
            revenda.RevendaId = Guid.NewGuid();
            _revendas.Add(revenda);
            return revenda.RevendaId;
        }

        public Guid AdicionarPedidoCliente(PedidoCliente pedido)
        {
            pedido.Id = Guid.NewGuid();
            _pedidosClientes.Add(pedido);
            return pedido.Id;
        }

        public List<PedidoCliente> ObterPedidosPorRevenda(Guid revendaId)
        {
            return _pedidosClientes;
        }
    }
}

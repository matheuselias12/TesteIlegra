using TesteIlegra.Domain.Modelo;
using TesteIlegra.Repositorio.Interface;
using TesteIlegra.Service.Interface;

namespace TesteIlegra.Service
{
    public class RevendaServico : IRevendaServico
    {
        private readonly IRevendaRepositorio _repositorio;
        private readonly IVebmaServico _vebmaServico;

        public RevendaServico(IRevendaRepositorio repositorio, IVebmaServico vebmaServico)
        {
            _repositorio = repositorio;
            _vebmaServico = vebmaServico;
        }

        public async Task<Guid> CadastrarRevendaAsync(Revenda revenda)
        {
            return await _repositorio.AdicionarAsync(revenda);
        }

        public async Task<Guid> ReceberPedidoClienteAsync(PedidoCliente pedido)
        {
            return await _repositorio.AdicionarAsync(pedido);
        }

        public async Task<bool> EmitirPedidoParaVebmaAsync(Guid revendaId, Guid pedidoId)
        {
            var revenda = await _repositorio.ObterPorIdAsync(revendaId);
            if (revenda is null)
            {
                return false;
            }

            var pedidoRevenda = await _repositorio.ObterPedidoPorIdAsync(pedidoId);

            var pedido = new PedidoRevenda()
            {
                Id = Guid.NewGuid(),
                RevendaId = revenda.RevendaId,
                Itens = pedidoRevenda.Itens
            };

            var total = pedido.Itens.Sum(i => i.Quantidade);
            if (total < 1000)
            {
                return false;
            }

            return _vebmaServico.EnviarPedidoAsync(pedido).Result.IsSuccessStatusCode;
        }
    }
}

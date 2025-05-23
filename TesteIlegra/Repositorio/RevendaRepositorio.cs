using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TesteIlegra.Data;
using TesteIlegra.Domain.Modelo;
using TesteIlegra.Repositorio.Interface;

namespace TesteIlegra.Repositorio
{
    [ExcludeFromCodeCoverage]
    public class RevendaRepositorio : IRevendaRepositorio
    {
        private readonly AppDbContext _contexto;

        public RevendaRepositorio(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Guid> AdicionarAsync(Revenda revenda)
        {
            try
            {
                await _contexto.Revendas.AddAsync(revenda);
                await _contexto.SaveChangesAsync();
                return revenda.RevendaId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Guid> AdicionarAsync(PedidoCliente pedidoCliente)
        {
            _contexto.PedidosClientes.Add(pedidoCliente);
            await _contexto.SaveChangesAsync();
            return pedidoCliente.Id;
        }
        public async Task<Revenda?> ObterPorIdAsync(Guid id)
        {
            return await _contexto.Revendas
                .Include(r => r.Contatos)
                .Include(r => r.EnderecosEntrega)
                .FirstOrDefaultAsync(r => r.RevendaId == id);
        }
        public async Task<PedidoCliente> ObterPedidoPorIdAsync(Guid pedidoId)
        {
            return await _contexto.PedidosClientes.Where(x => x.Id == pedidoId).Include(r => r.Itens).FirstOrDefaultAsync();
        }
    }
}

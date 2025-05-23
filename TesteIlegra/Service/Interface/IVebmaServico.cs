using TesteIlegra.Domain.Modelo;

namespace TesteIlegra.Service.Interface
{
    public interface IVebmaServico
    {
        Task<HttpResponseMessage> EnviarPedidoAsync(PedidoRevenda pedido);
    }
}

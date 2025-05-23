using Microsoft.AspNetCore.Mvc;
using TesteIlegra.Domain.Modelo;

namespace TesteIlegra.API.Controllers
{
    [ApiController]
    public class MockVebmaController : ControllerBase
    {
        [HttpPost("api/pedidos")]
        public IActionResult ReceberPedidoRevenda([FromBody] PedidoRevenda pedido)
        {
            if (pedido.Itens.Sum(i => i.Quantidade) < 1000)
            {
                return BadRequest("Pedido mínimo não atingido (1000 unidades).");
            }

            return Ok(new
            {
                Mensagem = "Pedido recebido com sucesso pela Ambev",
                PedidoId = Guid.NewGuid()
            });
        }
    }
}

using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using TesteIlegra.Domain.Modelo;
using TesteIlegra.Service.Interface;

namespace TesteIlegra.API.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("api/revendas")]
    public class RevendaController : ControllerBase
    {
        private readonly IRevendaServico _servico;

        public RevendaController(IRevendaServico servico)
        {
            _servico = servico;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] Revenda revenda)
        {
            var id = await _servico.CadastrarRevendaAsync(revenda);
            return Ok(new { Id = id });
        }

        [HttpPost("pedido-cliente")]
        public async Task<IActionResult> ReceberPedido([FromBody] PedidoCliente pedido)
        {
            var id = await _servico.ReceberPedidoClienteAsync(pedido);
            return Ok(new { Id = id, Itens = pedido.Itens });
        }

        [HttpPost("{revendaId}/emitir-pedido-vebma")]
        public async Task<IActionResult> EmitirPedido(Guid revendaId, Guid pedidoId)
        {
            var sucesso = await _servico.EmitirPedidoParaVebmaAsync(revendaId, pedidoId);
            return sucesso ? Ok("Pedido enviado com sucesso.") : BadRequest("Erro ao enviar pedido");
        }
    }
}

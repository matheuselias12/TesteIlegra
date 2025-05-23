using Polly.CircuitBreaker;
using System.Net.Http;
using TesteIlegra.Domain.Modelo;
using TesteIlegra.Service.Interface;

namespace TesteIlegra.Service
{
    public class VebmaServicoHttp : IVebmaServico
    {
        private readonly HttpClient _http;
        private readonly ILogger<VebmaServicoHttp> _logger;

        public VebmaServicoHttp(IHttpClientFactory factory, ILogger<VebmaServicoHttp> logger)
        {
            _http = factory.CreateClient("Vebma");
            _logger = logger;
        }

        public async Task<HttpResponseMessage> EnviarPedidoAsync(PedidoRevenda pedido)
        {
            try
            {
                _logger.LogWarning("Início do processo de envio do pedido para a Ambev.");

                var resposta = await _http.PostAsJsonAsync("/api/pedidos", pedido);

                if (!resposta.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Pedido não aceito pela Ambev. Status: {StatusCode}", resposta.StatusCode);
                }

                return resposta;
            }
            catch (BrokenCircuitException ex)
            {
                _logger.LogError(ex, "Circuito aberto: falha ao acessar o serviço da Ambev.");
                throw new Exception($"Circuito aberto: falha ao acessar o serviço da Ambev. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao enviar pedido à Ambev.");
                throw;
            }
        }
    }
}

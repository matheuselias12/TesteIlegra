using Microsoft.Extensions.Logging;
using Moq;
using Polly.CircuitBreaker;
using System.Net;
using TesteIlegra.Domain.Modelo;
using TesteIlegra.Service;

namespace TesteIlegraTest.Service
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

        public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }
    public class VebmaServicoHttpTest
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<ILogger<VebmaServicoHttp>> _loggerMock;

        public VebmaServicoHttpTest()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _loggerMock = new Mock<ILogger<VebmaServicoHttp>>();
        }

        [Fact]
        public async Task EnviarPedidoAsync_DeveRetornarSucesso_QuandoRespostaOk()
        {
            var handler = new MockHttpMessageHandler((request, token) =>
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:5001/mock/ambev")
            };

            _httpClientFactoryMock.Setup(f => f.CreateClient("Vebma")).Returns(httpClient);

            var servico = new VebmaServicoHttp(_httpClientFactoryMock.Object, _loggerMock.Object);

            var pedido = new PedidoRevenda
            {
                Id = Guid.NewGuid(),
                RevendaId = Guid.NewGuid(),
                Itens = new List<ItemPedido> { new() { Produto = "Cerveja", Quantidade = 1000 } }
            };

            var resposta = await servico.EnviarPedidoAsync(pedido);

            Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
        }

        [Fact]
        public async Task EnviarPedidoAsync_DeveLancarException_QuandoCircuitoAberto()
        {
            // Arrange
            var handler = new MockHttpMessageHandler((request, token) =>
            {
                throw new BrokenCircuitException("Circuito aberto.");
            });

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:5001/mock/ambev")
            };

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(f => f.CreateClient("Vebma")).Returns(httpClient);

            var loggerMock = new Mock<ILogger<VebmaServicoHttp>>();
            var servico = new VebmaServicoHttp(httpClientFactoryMock.Object, loggerMock.Object);

            var pedido = new PedidoRevenda
            {
                Id = Guid.NewGuid(),
                RevendaId = Guid.NewGuid(),
                Itens = new List<ItemPedido> { new ItemPedido { Produto = "Produto A", Quantidade = 1000 } }
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => servico.EnviarPedidoAsync(pedido));
            Assert.Contains("Circuito aberto", ex.Message);
        }
        [Fact]
        public async Task EnviarPedidoAsync_DeveLancarException_Generica()
        {
            // Arrange
            var handler = new MockHttpMessageHandler((request, token) =>
            {
                throw new InvalidOperationException("Erro genérico.");
            });

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:5001/mock/ambev")
            };

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(f => f.CreateClient("Vebma")).Returns(httpClient);

            var loggerMock = new Mock<ILogger<VebmaServicoHttp>>();
            var servico = new VebmaServicoHttp(httpClientFactoryMock.Object, loggerMock.Object);

            var pedido = new PedidoRevenda
            {
                Id = Guid.NewGuid(),
                RevendaId = Guid.NewGuid(),
                Itens = new List<ItemPedido> { new ItemPedido { Produto = "Produto A", Quantidade = 1000 } }
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => servico.EnviarPedidoAsync(pedido));
        }
        public class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handler;

            public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
            {
                _handler = handler;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return _handler(request, cancellationToken);
            }
        }
    }
}

using Moq;
using System.Net;
using TesteIlegra.Domain.Modelo;
using TesteIlegra.Repositorio.Interface;
using TesteIlegra.Service;
using TesteIlegra.Service.Interface;

namespace TesteIlegraTest.Service
{
    public class RevendaServicoTests
    {
        private readonly Mock<IRevendaRepositorio> _repositorioMock;
        private readonly Mock<IVebmaServico> _vebmaMock;
        private readonly RevendaServico _servico;

        public RevendaServicoTests()
        {
            _repositorioMock = new Mock<IRevendaRepositorio>();
            _vebmaMock = new Mock<IVebmaServico>();
            _servico = new RevendaServico(_repositorioMock.Object, _vebmaMock.Object);
        }

        [Fact]
        public async Task CadastrarRevendaAsync_DeveRetornarId()
        {
            var revenda = new Revenda { Cnpj = "123", RazaoSocial = "Teste", NomeFantasia = "Teste", Email = "teste@teste.com" };
            var idEsperado = Guid.NewGuid();
            _repositorioMock.Setup(r => r.AdicionarAsync(revenda)).ReturnsAsync(idEsperado);

            var id = await _servico.CadastrarRevendaAsync(revenda);

            Assert.Equal(idEsperado, id);
        }

        [Fact]
        public async Task ReceberPedidoClienteAsync_DeveRetornarId()
        {
            var pedido = new PedidoCliente { Cliente = "Cliente Teste", Itens = new List<ItemPedido>() };
            var idEsperado = Guid.NewGuid();
            _repositorioMock.Setup(r => r.AdicionarAsync(pedido)).ReturnsAsync(idEsperado);

            var id = await _servico.ReceberPedidoClienteAsync(pedido);

            Assert.Equal(idEsperado, id);
        }

        [Fact]
        public async Task EmitirPedidoParaVebmaAsync_DeveRetornarFalse_SeRevendaNaoExiste()
        {
            _repositorioMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync((Revenda)null);

            var resultado = await _servico.EmitirPedidoParaVebmaAsync(Guid.NewGuid(), Guid.NewGuid());

            Assert.False(resultado);
        }

        [Fact]
        public async Task EmitirPedidoParaVebmaAsync_DeveRetornarFalse_SeTotalItensMenorQue1000()
        {
            var revenda = new Revenda { RevendaId = Guid.NewGuid() };
            var pedidoCliente = new PedidoCliente { Itens = new List<ItemPedido> { new ItemPedido { Produto = "Água", Quantidade = 500 } } };

            _repositorioMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(revenda);
            _repositorioMock.Setup(r => r.ObterPedidoPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(pedidoCliente);

            var resultado = await _servico.EmitirPedidoParaVebmaAsync(Guid.NewGuid(), Guid.NewGuid());

            Assert.False(resultado);
        }

        [Fact]
        public async Task EmitirPedidoParaVebmaAsync_DeveRetornarTrue_SeTotalItensMaiorQue1000()
        {
            var revenda = new Revenda { RevendaId = Guid.NewGuid() };
            var pedidoCliente = new PedidoCliente
            {
                Itens = new List<ItemPedido>
            {
                new ItemPedido { Produto = "Cerveja", Quantidade = 800 },
                new ItemPedido { Produto = "Refrigerante", Quantidade = 300 }
            }
            };

            _repositorioMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(revenda);
            _repositorioMock.Setup(r => r.ObterPedidoPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(pedidoCliente);
            _vebmaMock.Setup(v => v.EnviarPedidoAsync(It.IsAny<PedidoRevenda>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var resultado = await _servico.EmitirPedidoParaVebmaAsync(Guid.NewGuid(), Guid.NewGuid());

            Assert.True(resultado);
        }
    }
}
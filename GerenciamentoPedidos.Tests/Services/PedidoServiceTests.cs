using GerenciamentoPedidos.Application.Interfaces;
using GerenciamentoPedidos.Application.Services;
using GerenciamentoPedidos.Domain.Contantes;
using GerenciamentoPedidos.Domain.Entities;
using GerenciamentoPedidos.Domain.Interfaces;
using Moq;

namespace GerenciamentoPedidos.Tests.Services
{
    [TestFixture]
    public class PedidoServiceTests
    {
        private Mock<IPedidoRepository> _pedidoRepositoryMock;
        private IPedidoService _pedidoService;

        [SetUp]
        public void Setup()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _pedidoService = new PedidoService(_pedidoRepositoryMock.Object);
        }

        [Test]
        public async Task AoIniciarPedidoAsync_DeveRetornarPedidoId()
        {
            // Arrange
            var novoPedido = new Pedido { Id = 1, CreatedAt = DateTime.UtcNow };
            _pedidoRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Pedido>())).Callback<Pedido>(pedido => pedido.Id = novoPedido.Id).Returns(Task.CompletedTask);

            // Act
            var pedidoId = await _pedidoService.IniciarPedidoAsync();

            // Assert
            Assert.AreEqual(novoPedido.Id, pedidoId);
            _pedidoRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Pedido>()), Times.Once);
        }

        [Test]
        public async Task AoAdicionarProdutoAoPedidoAsync_DeveAdicionarProduto()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, CreatedAt = DateTime.UtcNow };
            var produto = new ItemPedido { ProdutoId = 2, Quantidade = 1 };
            _pedidoRepositoryMock.Setup(repo => repo.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);

            // Act
            await _pedidoService.AdicionarProdutoAoPedidoAsync(pedido.Id, produto.ProdutoId, produto.Quantidade);

            // Assert
            Assert.IsTrue(pedido.Itens.Any(i => i.ProdutoId == produto.ProdutoId && i.Quantidade == produto.Quantidade));
            _pedidoRepositoryMock.Verify(repo => repo.UpdateAsync(pedido), Times.Once);
        }

        [Test]
        public void AoAdicionarProdutoAoPedidoAsync_DeveLancarExcecaoParaPedidoFechado()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, CreatedAt = DateTime.UtcNow, IsClosed = true };
            _pedidoRepositoryMock.Setup(repo => repo.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _pedidoService.AdicionarProdutoAoPedidoAsync(pedido.Id, 2, 1));
            Assert.AreEqual(ResponseMessages.NaoPossivelAddProdutosPedidoFechado, ex.Message);
        }

        [Test]
        public async Task AoRemoverProdutoDoPedidoAsync_DeveRemoverProduto()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, CreatedAt = DateTime.UtcNow };
            var produto = new ItemPedido { Id = 1, ProdutoId = 2, Quantidade = 1 };
            pedido.Itens.Add(produto);
            _pedidoRepositoryMock.Setup(repo => repo.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);

            // Act
            await _pedidoService.RemoverProdutoDoPedidoAsync(pedido.Id, produto.ProdutoId);

            // Assert
            Assert.IsEmpty(pedido.Itens);
            _pedidoRepositoryMock.Verify(repo => repo.UpdateAsync(pedido), Times.Once);
        }

        [Test]
        public void AoRemoverProdutoDoPedidoAsync_DeveLancarExcecaoParaPedidoFechado()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, CreatedAt = DateTime.UtcNow, IsClosed = true };
            _pedidoRepositoryMock.Setup(repo => repo.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _pedidoService.RemoverProdutoDoPedidoAsync(pedido.Id, 2));
            Assert.AreEqual(ResponseMessages.NaoPossivelRemoverProdutosPedidoFechado, ex.Message);
        }

        [Test]
        public async Task AoFecharPedidoAsync_DeveFecharPedido()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, CreatedAt = DateTime.UtcNow };
            var produto = new ItemPedido { ProdutoId = 2, Quantidade = 1 };
            pedido.Itens.Add(produto);
            _pedidoRepositoryMock.Setup(repo => repo.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);

            // Act
            await _pedidoService.FecharPedidoAsync(pedido.Id);

            // Assert
            Assert.IsTrue(pedido.IsClosed);
            _pedidoRepositoryMock.Verify(repo => repo.UpdateAsync(pedido), Times.Once);
        }

        [Test]
        public void AoFecharPedidoAsync_DeveLancarExcecaoParaPedidoSemProdutos()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, CreatedAt = DateTime.UtcNow };
            _pedidoRepositoryMock.Setup(repo => repo.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _pedidoService.FecharPedidoAsync(pedido.Id));
            Assert.AreEqual(ResponseMessages.NaoPossivelFecharPedidoSemProduto, ex.Message);
        }

        [Test]
        public async Task AoObterPedidoPorIdAsync_DeveRetornarPedido()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, CreatedAt = DateTime.UtcNow };
            _pedidoRepositoryMock.Setup(repo => repo.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);

            // Act
            var result = await _pedidoService.ObterPedidoPorIdAsync(pedido.Id);

            // Assert
            Assert.AreEqual(pedido, result);
        }

        [Test]
        public void AoObterPedidoPorIdAsync_DeveLancarExcecaoParaPedidoNaoEncontrado()
        {
            // Arrange
            _pedidoRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Pedido)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _pedidoService.ObterPedidoPorIdAsync(1));
            Assert.AreEqual(ResponseMessages.PedidoNaoEncontrado, ex.Message);
        }
    }
}

using GerenciamentoPedidos.Application.Interfaces;
using GerenciamentoPedidos.Domain.Contantes;
using GerenciamentoPedidos.Domain.Entities;
using GerenciamentoPedidos.Domain.Interfaces;

namespace GerenciamentoPedidos.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<int> IniciarPedidoAsync()
        {
            var pedido = new Pedido { CreatedAt = DateTime.UtcNow };
            await _pedidoRepository.AddAsync(pedido);
            return pedido.Id;
        }

        public async Task AdicionarProdutoAoPedidoAsync(int pedidoId, int produtoId, int quantidade)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
            pedido.AdicionarProduto(new ItemPedido { ProdutoId = produtoId, Quantidade = quantidade });
            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task RemoverProdutoDoPedidoAsync(int pedidoId, int produtoId)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
            pedido.RemoverProduto(produtoId);
            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task FecharPedidoAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
            pedido.Close();
            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task<Pedido> ObterPedidoPorIdAsync(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
            {
                throw new KeyNotFoundException(ResponseMessages.PedidoNaoEncontrado);
            }
            return pedido;
        }

        public async Task<List<Pedido>> ListarPedidosAsync()
        {
            return await _pedidoRepository.GetAllAsync();
        }
    }
}

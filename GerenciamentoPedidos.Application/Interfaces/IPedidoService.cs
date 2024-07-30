using GerenciamentoPedidos.Domain.Entities;

namespace GerenciamentoPedidos.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<int> IniciarPedidoAsync();
        Task AdicionarProdutoAoPedidoAsync(int pedidoId, int produtoId, int quantidade);
        Task RemoverProdutoDoPedidoAsync(int pedidoId, int produtoId);
        Task FecharPedidoAsync(int pedidoId);
        Task<Pedido> ObterPedidoPorIdAsync(int id);
        Task<List<Pedido>> ListarPedidosAsync();
    }
}

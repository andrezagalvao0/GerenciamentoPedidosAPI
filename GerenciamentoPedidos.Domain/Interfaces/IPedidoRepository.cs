using GerenciamentoPedidos.Domain.Entities;

namespace GerenciamentoPedidos.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> GetByIdAsync(int id);
        Task<List<Pedido>> GetAllAsync();
        Task AddAsync(Pedido pedido);
        Task UpdateAsync(Pedido pedido);
    }
}

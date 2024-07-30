using GerenciamentoPedidos.Domain.Entities;
using GerenciamentoPedidos.Domain.Interfaces;
using GerenciamentoPedidos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPedidos.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly GerenciamentoPedidosContext _context;

        public PedidoRepository(GerenciamentoPedidosContext context)
        {
            _context = context;
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            return await _context.Pedidos.Include(o => o.Itens).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos.Include(o => o.Itens).ToListAsync();
        }

        public async Task AddAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
    }
}

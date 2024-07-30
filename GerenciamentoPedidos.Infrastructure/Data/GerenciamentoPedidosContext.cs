using GerenciamentoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPedidos.Infrastructure.Data
{
    public class GerenciamentoPedidosContext : DbContext
    {
        public GerenciamentoPedidosContext(DbContextOptions<GerenciamentoPedidosContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GerenciamentoPedidosContext).Assembly);
        }
    }
}

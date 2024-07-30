using GerenciamentoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoPedidos.Infrastructure.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CreatedAt).IsRequired();
            builder.Property(o => o.IsClosed).IsRequired();
            builder.HasMany(o => o.Itens).WithOne().HasForeignKey(i => i.PedidoId);
        }
    }
}

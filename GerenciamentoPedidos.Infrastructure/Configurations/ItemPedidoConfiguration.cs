using GerenciamentoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoPedidos.Infrastructure.Configurations
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder) 
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ProdutoId).IsRequired();
            builder.Property(i => i.Quantidade).IsRequired();
        }
    }
}

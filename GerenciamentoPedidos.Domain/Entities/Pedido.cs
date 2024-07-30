using GerenciamentoPedidos.Domain.Contantes;

namespace GerenciamentoPedidos.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsClosed { get; set; }
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        public void AdicionarProduto(ItemPedido item)
        {
            if (IsClosed)
                throw new InvalidOperationException(ResponseMessages.NaoPossivelAddProdutosPedidoFechado);

            Itens.Add(item);
        }

        public void RemoverProduto(int produtoId)
        {
            if (IsClosed)
                throw new InvalidOperationException(ResponseMessages.NaoPossivelRemoverProdutosPedidoFechado);

            var item = Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item != null)
            {
                Itens.Remove(item);
            }
            else { throw new InvalidOperationException(ResponseMessages.PedidoNaoEncontrado); }
                
        }

        public void Close()
        {
            if (Itens.Count == 0 || Itens.Any(i => i.ProdutoId == 0 || i.Quantidade == 0))
                throw new InvalidOperationException(ResponseMessages.NaoPossivelFecharPedidoSemProduto);

            IsClosed = true;
        }
    }
}

namespace GerenciamentoPedidos.Domain.Contantes
{
    public static class ResponseMessages
    {
        public const string PedidoFechadoComSucesso = "Pedido fechado com sucesso.";
        public const string ProdutoAddComSucesso = "Produto adicionado com sucesso.";
        public const string ProdutoRemovidoComSucesso = "Produto removido com sucesso.";
        public const string NaoPossivelAddProdutosPedidoFechado = "Não é possível adicionar produtos a um pedido fechado.";
        public const string NaoPossivelRemoverProdutosPedidoFechado = "Não é possível remover produtos de um pedido fechado.";
        public const string NaoPossivelFecharPedidoSemProduto = "Não é possível fechar um pedido sem ao menos um produto.";
        public const string PedidoNaoEncontrado = "Pedido não encontrado.";
        public const string ProdutoNaoEncontrado = "Produto não encontrado no pedido.";
    }
}

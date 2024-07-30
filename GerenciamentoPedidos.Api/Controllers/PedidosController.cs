using GerenciamentoPedidos.Api.Dtos;
using GerenciamentoPedidos.Application.Interfaces;
using GerenciamentoPedidos.Domain.Contantes;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPedidos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost("Iniciar")]
        public async Task<IActionResult> IniciarPedido()
        {
            var pedidoId = await _pedidoService.IniciarPedidoAsync();
            return Ok(new { Message = $"Pedido iniciado com ID: {pedidoId}" });
        }

        [HttpPost("AdicionarProduto/{pedidoId}")]
        public async Task<IActionResult> AdicionarProdutoAoPedido(int pedidoId, [FromBody] AdicionarProdutoDto dto)
        {
            try
            {
                await _pedidoService.AdicionarProdutoAoPedidoAsync(pedidoId, dto.ProdutoId, dto.Quantidade);
                return Ok(new { Message = ResponseMessages.ProdutoAddComSucesso });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            
        }

        [HttpDelete("RemoverProduto/{pedidoId}/{produtoId}")]
        public async Task<IActionResult> RemoverProdutoDoPedido(int pedidoId, int produtoId)
        {
            try
            {
                await _pedidoService.RemoverProdutoDoPedidoAsync(pedidoId, produtoId);
                return Ok(new { Message = ResponseMessages.ProdutoRemovidoComSucesso });
            } 
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            
        }

        [HttpPost("Fechar/{pedidoId}")]
        public async Task<IActionResult> FecharPedido(int pedidoId)
        {
            try
            {
                await _pedidoService.FecharPedidoAsync(pedidoId);
                return Ok(new {Message = ResponseMessages.PedidoFechadoComSucesso });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("ListarTodos")]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _pedidoService.ListarPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("ObterPorId/{id}")]   
        public async Task<IActionResult> ObterPedidoPorId(int id)
        {
            try
            {
                var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);
                return Ok(pedido);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}

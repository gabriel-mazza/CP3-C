using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmaciaDigital.Data;
using FarmaciaDigital.DTOs;
using FarmaciaDigital.Models;
using FarmaciaDigital.Services;

namespace FarmaciaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdesoesController : ControllerBase
    {
        private readonly FarmaciaContext _context;
        private readonly IPlanoSaudeService _planoService;

        public AdesoesController(FarmaciaContext context, IPlanoSaudeService planoService)
        {
            _context = context;
            _planoService = planoService;
        }

        [HttpPost]
        public async Task<IActionResult> Solicitar([FromBody] CriarAdesaoDTO dto)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Filial)
                .FirstOrDefaultAsync(c => c.Id == dto.ClienteId);

            if (cliente is null)
                return NotFound(new { message = $"Cliente {dto.ClienteId} não encontrado." });

            var produto = await _context.Produtos.FindAsync(dto.ProdutoId);
            if (produto is null)
                return NotFound(new { message = $"Plano {dto.ProdutoId} não encontrado." });

            var adesao = new Adesao
            {
                ClienteId = cliente.Id,
                ProdutoId = produto.Id,
                DataAdesao = DateTime.UtcNow,
                Status = StatusAdesao.Aprovada
            };

            
            if (produto is PlanoSaude plano)
            {
                adesao.MensalidadeEfetiva = _planoService.CalcularMensalidadeEfetiva(plano, cliente);
                adesao.Observacao = _planoService.GerarObservacao(plano, cliente, adesao.MensalidadeEfetiva.Value);
            }

            _context.Adesoes.Add(adesao);
            await _context.SaveChangesAsync();

            await _context.Entry(adesao).Reference(a => a.Cliente).LoadAsync();
            await _context.Entry(adesao).Reference(a => a.Produto).LoadAsync();

            return CreatedAtAction(nameof(BuscarPorId), new { id = adesao.Id },
                MapToResponse(adesao));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var adesao = await _context.Adesoes
                .Include(a => a.Cliente)
                .Include(a => a.Produto)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (adesao is null)
                return NotFound(new { message = $"Adesão {id} não encontrada." });

            return Ok(MapToResponse(adesao));
        }

        private static AdesaoResponseDTO MapToResponse(Adesao a) => new()
        {
            Id = a.Id,
            ClienteId = a.ClienteId,
            NomeCliente = a.Cliente.Nome,
            ProdutoId = a.ProdutoId,
            NomeProduto = a.Produto.Nome,
            Status = a.Status.ToString(),
            MensalidadeEfetiva = a.MensalidadeEfetiva,
            Observacao = a.Observacao,
            DataAdesao = a.DataAdesao
        };
    }
}

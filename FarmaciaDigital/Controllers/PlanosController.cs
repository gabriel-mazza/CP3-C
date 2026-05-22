using Microsoft.AspNetCore.Mvc;
using FarmaciaDigital.Data;
using FarmaciaDigital.DTOs;
using FarmaciaDigital.Models;

namespace FarmaciaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanosController : ControllerBase
    {
        private readonly FarmaciaContext _context;
        public PlanosController(FarmaciaContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarPlanoSaudeDTO dto)
        {
            var plano = new PlanoSaude
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Cobertura = dto.Cobertura,
                MensalidadeBase = dto.MensalidadeBase,
                Ativo = true  
            };

            _context.PlanosSaude.Add(plano);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(BuscarPorId), new { id = plano.Id },
                new ProdutoResponseDTO
                {
                    Id = plano.Id,
                    Nome = plano.Nome,
                    Descricao = plano.Descricao,
                    Cobertura = plano.Cobertura,
                    MensalidadeBase = plano.MensalidadeBase
                });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var plano = await _context.PlanosSaude.FindAsync(id);
            if (plano is null)
                return NotFound(new { message = $"Plano {id} não encontrado." });

            return Ok(new ProdutoResponseDTO
            {
                Id = plano.Id,
                Nome = plano.Nome,
                Descricao = plano.Descricao,
                Cobertura = plano.Cobertura,
                MensalidadeBase = plano.MensalidadeBase
            });
        }
    }
}

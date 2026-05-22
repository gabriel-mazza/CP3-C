using Microsoft.AspNetCore.Mvc;
using FarmaciaDigital.DTOs;
using FarmaciaDigital.Interfaces;
using FarmaciaDigital.Models;

namespace FarmaciaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiliaisController : ControllerBase
    {
        private readonly IFilialRepository _repo;
        public FiliaisController(IFilialRepository repo) => _repo = repo;



        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarFilialDTO dto)
        {
            var filial = new Filial
            {
                Nome = dto.Nome,
                Endereco = dto.Endereco,
                Cidade = dto.Cidade
            };

            await _repo.AdicionarAsync(filial);

            return CreatedAtAction(nameof(BuscarPorId), new { id = filial.Id },
                new FilialResponseDTO
                {
                    Id = filial.Id,
                    Nome = filial.Nome,
                    Endereco = filial.Endereco,
                    Cidade = filial.Cidade
                });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var filial = await _repo.ObterPorIdAsync(id);
            if (filial is null)
                return NotFound(new { message = $"Filial {id} não encontrada." });

            return Ok(new FilialResponseDTO
            {
                Id = filial.Id,
                Nome = filial.Nome,
                Endereco = filial.Endereco,
                Cidade = filial.Cidade
            });
        }
    }
}

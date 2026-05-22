using Microsoft.AspNetCore.Mvc;
using FarmaciaDigital.DTOs;
using FarmaciaDigital.Interfaces;
using FarmaciaDigital.Models;

namespace FarmaciaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly IFilialRepository _filialRepo;

        public ClientesController(IClienteRepository clienteRepo, IFilialRepository filialRepo)
        {
            _clienteRepo = clienteRepo;
            _filialRepo = filialRepo;
        }

        [HttpPost("pf")]
        public async Task<IActionResult> CadastrarPF([FromBody] CriarPessoaFisicaDTO dto)
        {
            if (!await _filialRepo.ExisteFilialAsync(dto.FilialId))
                return NotFound(new { message = $"Filial {dto.FilialId} não encontrada." });

            if (await _clienteRepo.ExisteCpfAsync(dto.Cpf))
                return BadRequest("CPF já cadastrado.");

            var pf = new PessoaFisica
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone,
                Cpf = dto.Cpf,
                DataNascimento = dto.DataNascimento,
                FilialId = dto.FilialId
            };

            await _clienteRepo.AdicionarPFAsync(pf);

            return CreatedAtAction(nameof(BuscarPorId), new { id = pf.Id },
                new ClienteResponseDTO
                {
                    Id = pf.Id,
                    Nome = pf.Nome,
                    Email = pf.Email,
                    Telefone = pf.Telefone,
                    TipoCliente = "PF",
                    Documento = pf.Cpf,
                    FilialId = pf.FilialId
                });
        }

      
        [HttpPost("pj")]
        public async Task<IActionResult> CadastrarPJ([FromBody] CriarPessoaJuridicaDTO dto)
        {
            if (!await _filialRepo.ExisteFilialAsync(dto.FilialId))
                return NotFound(new { message = $"Filial {dto.FilialId} não encontrada." });

            if (await _clienteRepo.ExisteCnpjAsync(dto.Cnpj))
                return BadRequest("CNPJ já cadastrado.");

            var pj = new PessoaJuridica
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone,
                Cnpj = dto.Cnpj,
                RazaoSocial = dto.RazaoSocial,
                FilialId = dto.FilialId
            };

            await _clienteRepo.AdicionarPJAsync(pj);

            return CreatedAtAction(nameof(BuscarPorId), new { id = pj.Id },
                new ClienteResponseDTO
                {
                    Id = pj.Id,
                    Nome = pj.Nome,
                    Email = pj.Email,
                    Telefone = pj.Telefone,
                    TipoCliente = "PJ",
                    Documento = pj.Cnpj,
                    FilialId = pj.FilialId
                });
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var cliente = await _clienteRepo.ObterPorIdAsync(id);
            if (cliente is null)
                return NotFound(new { message = $"Cliente {id} não encontrado." });

            var (tipo, documento) = cliente switch
            {
                PessoaFisica pf => ("PF", pf.Cpf),
                PessoaJuridica pj => ("PJ", pj.Cnpj),
                _ => ("?", "?")
            };

            return Ok(new ClienteResponseDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                TipoCliente = tipo,
                Documento = documento,
                FilialId = cliente.FilialId,
                FilialNome = cliente.Filial?.Nome ?? ""
            });
        }
    }
}

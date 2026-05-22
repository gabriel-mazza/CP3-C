using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmaciaDigital.Controllers;
using FarmaciaDigital.Data;
using FarmaciaDigital.DTOs;
using FarmaciaDigital.Models;
using FarmaciaDigital.Repositories;
using FarmaciaDigital.Services;
using Xunit;

namespace FarmaciaDigital.Tests
{
    public class FarmaciaTests
    {
        private FarmaciaContext CriarContexto(string nome)
        {
            var options = new DbContextOptionsBuilder<FarmaciaContext>()
                .UseInMemoryDatabase(nome)
                .Options;
            return new FarmaciaContext(options);
        }

        [Fact]
        public async Task ExisteCpf_DeveRetornarTrue_QuandoCpfJaCadastrado()
        {
            using var db = CriarContexto("cpf_duplicado");
            db.PessoasFisicas.Add(new PessoaFisica
            {
                Nome = "João",
                Email = "j@j.com",
                Telefone = "11999",
                Cpf = "111.111.111-11",
                DataNascimento = new DateTime(1990, 1, 1),
                FilialId = 1
            });
            await db.SaveChangesAsync();

            var repo = new ClienteRepository(db);
            var existe = await repo.ExisteCpfAsync("111.111.111-11");

            existe.Should().BeTrue();
        }

        [Fact]
        public async Task ExisteCnpj_DeveRetornarTrue_QuandoCnpjJaCadastrado()
        {
            using var db = CriarContexto("cnpj_duplicado");
            db.PessoasJuridicas.Add(new PessoaJuridica
            {
                Nome = "Empresa",
                Email = "e@e.com",
                Telefone = "1133",
                Cnpj = "11.222.333/0001-44",
                RazaoSocial = "Empresa SA",
                FilialId = 1
            });
            await db.SaveChangesAsync();

            var repo = new ClienteRepository(db);
            var existe = await repo.ExisteCnpjAsync("11.222.333/0001-44");

            existe.Should().BeTrue();
        }

        [Fact]
        public async Task ExisteFilial_DeveRetornarFalse_QuandoFilialNaoExiste()
        {
            using var db = CriarContexto("filial_inexistente");
            var repo = new FilialRepository(db);

            var existe = await repo.ExisteFilialAsync(999);

            existe.Should().BeFalse();
        }


        [Fact]
        public async Task Adesao_DeveRetornar404_QuandoClienteInexistente()
        {
            using var db = CriarContexto("adesao_cliente_inexistente");
            var controller = new AdesoesController(db, new PlanoSaudeService());

            var result = await controller.Solicitar(new CriarAdesaoDTO
            {
                ClienteId = 999,
                ProdutoId = 1
            });

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        
        [Fact]
        public void PlanoSaude_PF_DevePagarMensalidadeBase()
        {
            var plano = new PlanoSaude
            {
                Id = 1,
                Nome = "Básico",
                Descricao = "",
                Ativo = true,
                Cobertura = "Básico",
                MensalidadeBase = 200.00m
            };
            var pf = new PessoaFisica
            {
                Nome = "João",
                Email = "j@j.com",
                Telefone = "11999",
                Cpf = "123",
                DataNascimento = new DateTime(1990, 1, 1),
                FilialId = 1
            };

            var service = new PlanoSaudeService();
            var mensalidade = service.CalcularMensalidadeEfetiva(plano, pf);

            mensalidade.Should().Be(200.00m);
        }

     
        [Fact]
        public void PlanoSaude_PFIdoso_DeveReceberAcrescimo20Porcento()
        {
            var plano = new PlanoSaude
            {
                Id = 1,
                Nome = "Básico",
                Descricao = "",
                Ativo = true,
                Cobertura = "Básico",
                MensalidadeBase = 200.00m
            };
            var pfIdoso = new PessoaFisica
            {
                Nome = "Maria",
                Email = "m@m.com",
                Telefone = "11888",
                Cpf = "456",
                DataNascimento = new DateTime(1950, 1, 1),
                FilialId = 1
            };

            var service = new PlanoSaudeService();
            var mensalidade = service.CalcularMensalidadeEfetiva(plano, pfIdoso);

           
            mensalidade.Should().Be(240.00m);
        }

        
        [Fact]
        public void PlanoSaude_PJ_DeveReceberDesconto15Porcento()
        {
            var plano = new PlanoSaude
            {
                Id = 1,
                Nome = "Básico",
                Descricao = "",
                Ativo = true,
                Cobertura = "Básico",
                MensalidadeBase = 200.00m
            };
            var pj = new PessoaJuridica
            {
                Nome = "Empresa",
                Email = "e@e.com",
                Telefone = "1133",
                Cnpj = "123",
                RazaoSocial = "SA",
                FilialId = 1
            };

            var service = new PlanoSaudeService();
            var mensalidade = service.CalcularMensalidadeEfetiva(plano, pj);

          
            mensalidade.Should().Be(170.00m);
        }
    }
}

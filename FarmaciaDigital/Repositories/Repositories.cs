using Microsoft.EntityFrameworkCore;
using FarmaciaDigital.Data;
using FarmaciaDigital.Interfaces;
using FarmaciaDigital.Models;

namespace FarmaciaDigital.Repositories
{
    public class FilialRepository : IFilialRepository
    {
        private readonly FarmaciaContext _context;
        public FilialRepository(FarmaciaContext context) => _context = context;

        public async Task<Filial> AdicionarAsync(Filial filial)
        {
            await _context.Filiais.AddAsync(filial);
            await _context.SaveChangesAsync();
            return filial;
        }

        public async Task<Filial?> ObterPorIdAsync(int id)
            => await _context.Filiais.FirstOrDefaultAsync(f => f.Id == id);

  
        public async Task<bool> ExisteFilialAsync(int id)
            => await _context.Filiais.CountAsync(f => f.Id == id) > 0;
    }

    public class ClienteRepository : IClienteRepository
    {
        private readonly FarmaciaContext _context;
        public ClienteRepository(FarmaciaContext context) => _context = context;

        public async Task<PessoaFisica> AdicionarPFAsync(PessoaFisica pf)
        {
            await _context.PessoasFisicas.AddAsync(pf);
            await _context.SaveChangesAsync();
            return pf;
        }

        public async Task<PessoaJuridica> AdicionarPJAsync(PessoaJuridica pj)
        {
            await _context.PessoasJuridicas.AddAsync(pj);
            await _context.SaveChangesAsync();
            return pj;
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
            => await _context.Clientes.Include(c => c.Filial).FirstOrDefaultAsync(c => c.Id == id);


        public async Task<bool> ExisteCpfAsync(string cpf)
            => await _context.PessoasFisicas.CountAsync(p => p.Cpf == cpf) > 0;

        public async Task<bool> ExisteCnpjAsync(string cnpj)
            => await _context.PessoasJuridicas.CountAsync(p => p.Cnpj == cnpj) > 0;
    }
}

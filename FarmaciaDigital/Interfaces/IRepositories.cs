using FarmaciaDigital.Models;

namespace FarmaciaDigital.Interfaces
{
    public interface IFilialRepository
    {
        Task<Filial> AdicionarAsync(Filial filial);
        Task<Filial?> ObterPorIdAsync(int id);
        Task<bool> ExisteFilialAsync(int id);
    }

    public interface IClienteRepository
    {
        Task<PessoaFisica> AdicionarPFAsync(PessoaFisica pf);
        Task<PessoaJuridica> AdicionarPJAsync(PessoaJuridica pj);
        Task<Cliente?> ObterPorIdAsync(int id);
        Task<bool> ExisteCpfAsync(string cpf);
        Task<bool> ExisteCnpjAsync(string cnpj);
    }
}

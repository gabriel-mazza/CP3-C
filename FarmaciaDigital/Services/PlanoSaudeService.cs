using FarmaciaDigital.Models;

namespace FarmaciaDigital.Services
{

    public interface IPlanoSaudeService
    {
        decimal CalcularMensalidadeEfetiva(PlanoSaude plano, Cliente cliente);
        string GerarObservacao(PlanoSaude plano, Cliente cliente, decimal mensalidadeEfetiva);
    }

    public class PlanoSaudeService : IPlanoSaudeService
    {
        private const decimal AcrescimoIdoso = 0.20m;   
        private const decimal DescontoPJ = 0.15m;        

        public decimal CalcularMensalidadeEfetiva(PlanoSaude plano, Cliente cliente)
        {
            if (cliente is PessoaJuridica)
                return Math.Round(plano.MensalidadeBase * (1 - DescontoPJ), 2);

            if (cliente is PessoaFisica pf)
            {
                var idade = CalcularIdade(pf.DataNascimento);
                if (idade >= 60)
                    return Math.Round(plano.MensalidadeBase * (1 + AcrescimoIdoso), 2);
            }

            return plano.MensalidadeBase;
        }

        public string GerarObservacao(PlanoSaude plano, Cliente cliente, decimal mensalidadeEfetiva)
        {
            if (cliente is PessoaJuridica)
                return $"Mensalidade: R$ {mensalidadeEfetiva:F2} (PJ — 15% de desconto sobre base R$ {plano.MensalidadeBase:F2})";

            if (cliente is PessoaFisica pf)
            {
                var idade = CalcularIdade(pf.DataNascimento);
                if (idade >= 60)
                    return $"Mensalidade: R$ {mensalidadeEfetiva:F2} (PF idoso {idade} anos — 20% de acréscimo sobre base R$ {plano.MensalidadeBase:F2})";
            }

            return $"Mensalidade: R$ {mensalidadeEfetiva:F2} (taxa base)";
        }

        private static int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }
    }
}

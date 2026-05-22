namespace FarmaciaDigital.Models
{
    public abstract class Cliente
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }

        public int FilialId { get; set; }
        public Filial Filial { get; set; } = null!;

        public ICollection<Adesao> Adesoes { get; set; } = new List<Adesao>();
    }

    public class PessoaFisica : Cliente
    {
        public required string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public class PessoaJuridica : Cliente
    {
        public required string Cnpj { get; set; }
        public required string RazaoSocial { get; set; }
    }
}

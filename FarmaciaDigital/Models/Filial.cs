namespace FarmaciaDigital.Models
{
    public class Filial
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Endereco { get; set; }
        public required string Cidade { get; set; }

        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}

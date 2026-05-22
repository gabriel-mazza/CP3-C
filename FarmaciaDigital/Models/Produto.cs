namespace FarmaciaDigital.Models
{
    public abstract class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true; 

        public ICollection<Adesao> Adesoes { get; set; } = new List<Adesao>();
    }

    public class PlanoSaude : Produto
    {
        public required string Cobertura { get; set; }
        public decimal MensalidadeBase { get; set; }
    }
}

namespace FarmaciaDigital.Models
{
    public enum StatusAdesao
    {
        Pendente = 0,
        Aprovada = 1,
        Recusada = 2
    }

    public class Adesao
    {
        public int Id { get; set; }
        public DateTime DataAdesao { get; set; } = DateTime.UtcNow;
        public StatusAdesao Status { get; set; } = StatusAdesao.Pendente;
        public string? Observacao { get; set; }


        public decimal? MensalidadeEfetiva { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
    }
}

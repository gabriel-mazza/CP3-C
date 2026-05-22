using Microsoft.EntityFrameworkCore;
using FarmaciaDigital.Models;

namespace FarmaciaDigital.Data
{
    public class FarmaciaContext : DbContext
    {
        public FarmaciaContext(DbContextOptions<FarmaciaContext> options) : base(options) { }

        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<PlanoSaude> PlanosSaude { get; set; }
        public DbSet<Adesao> Adesoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

      
            modelBuilder.Entity<Cliente>()
                .HasDiscriminator<string>("TipoCliente")
                .HasValue<PessoaFisica>("PF")
                .HasValue<PessoaJuridica>("PJ");

            modelBuilder.Entity<PessoaFisica>()
                .HasIndex(p => p.Cpf).IsUnique();

            modelBuilder.Entity<PessoaJuridica>()
                .HasIndex(p => p.Cnpj).IsUnique();

            
            modelBuilder.Entity<Produto>()
                .HasDiscriminator<string>("TipoProduto")
                .HasValue<PlanoSaude>("PLANO_SAUDE");

            
            modelBuilder.Entity<Produto>()
                .Property(p => p.Ativo)
                .HasColumnType("NUMBER(1)")
                .HasConversion<int>();

            modelBuilder.Entity<PlanoSaude>()
                .Property(p => p.MensalidadeBase)
                .HasColumnType("NUMBER(10,2)");

            
            modelBuilder.Entity<Adesao>(e =>
            {
                e.Property(a => a.Status).HasConversion<string>();

                e.Property(a => a.MensalidadeEfetiva)
                 .HasColumnType("NUMBER(10,2)");

                e.HasOne(a => a.Cliente)
                 .WithMany(c => c.Adesoes)
                 .HasForeignKey(a => a.ClienteId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(a => a.Produto)
                 .WithMany(p => p.Adesoes)
                 .HasForeignKey(a => a.ProdutoId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

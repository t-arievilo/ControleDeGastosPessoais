using Microsoft.EntityFrameworkCore;
using ControleGastos.Domain.Entidades;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração Pessoa
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.HasMany(e => e.Transacoes).WithOne(e => e.Pessoa).HasForeignKey(e => e.PessoaId).OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Finalidade).IsRequired().HasConversion<int>();
            });

            // Configuração Transacao
            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Valor).IsRequired(); // SQLite gerencia decimal como NUMERIC automaticamente
                entity.Property(e => e.Tipo).IsRequired().HasConversion<int>();
                
                entity.HasIndex(e => e.PessoaId);
                entity.HasIndex(e => e.DataCriacao);
            });

            // Dados iniciais com IDs FIXOS (Evita erros de Migrations)
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria(new Guid("11111111-1111-1111-1111-111111111111"), "Alimentação", FinalidadeCategoria.Despesa),
                new Categoria(new Guid("22222222-2222-2222-2222-222222222222"), "Investimentos", FinalidadeCategoria.Ambas),
                new Categoria(new Guid("33333333-3333-3333-3333-333333333333"), "Salário", FinalidadeCategoria.Receita),
                new Categoria(new Guid("44444444-4444-4444-4444-444444444444"), "Lazer", FinalidadeCategoria.Despesa)
            );
        }
    }
}
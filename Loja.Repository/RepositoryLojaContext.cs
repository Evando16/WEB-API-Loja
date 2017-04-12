using Loja.ClasseLibrary;
using System.Configuration;
using System.Data.Entity;

namespace Loja.Repository
{
    public class RepositoryLojaContext : DbContext
    {
        public RepositoryLojaContext() :
            base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {

        }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItemVendas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>()
                .HasRequired(p => p.Pessoa)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vendedor>()
                .HasRequired(p => p.Pessoa)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}


using Loja.ClasseLibray;
using System.Data.Entity;

namespace Loja.DataLayer
{
    public class LojaModelContext : DbContext
    {
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        public bool InserirPessoa(Pessoa pessoa)
        {
            Pessoas.Add(pessoa);
            return true;
        }
    }
}

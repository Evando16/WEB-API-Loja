using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.ClasseLibrary
{
    [Table("Estoque")]
    public class Estoque
    {
        public int Id { get; set; }        
        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.ClasseLibrary
{
    [Table("Produto")]
    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; set; }        
        public int FornecedorId { get; set; }
        [ForeignKey("FornecedorId")]
        public virtual Fornecedor Fornecedor { get; set; }
        public float Valor { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
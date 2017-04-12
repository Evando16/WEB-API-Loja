using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.ClasseLibrary
{
    [Table("ItemVenda")]
    public class ItemVenda
    {
        public int Id { get; set; }

        public int VendaId { get; set; }
        [ForeignKey("VendaId")]
        public virtual Venda Venda { get; set; }

        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }

        public int Quantidade { get; set; }
        public int ValorUnitario { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}

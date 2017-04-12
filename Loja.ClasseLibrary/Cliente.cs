using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.ClasseLibrary
{
    [Table("Cliente")]
    public class Cliente
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }

    }
}

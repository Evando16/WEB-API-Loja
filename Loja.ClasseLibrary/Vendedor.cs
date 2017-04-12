using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.ClasseLibrary
{
    [Table("Vendedor")]
    public class Vendedor
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public int PessoaId { get; set; }
        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }
    }
}

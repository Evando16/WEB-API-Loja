using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.ClasseLibrary
{
    [Table("Fornecedor")]
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
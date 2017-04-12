using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.ClasseLibrary
{
    [Table("Pessoa")]
    public class Pessoa
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

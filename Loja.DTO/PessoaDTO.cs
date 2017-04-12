using System;

namespace Loja.DTO
{
    public class PessoaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

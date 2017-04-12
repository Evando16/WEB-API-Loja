using System;

namespace Loja.DTO
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int FornecedorId { get; set; }
        public float Valor { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
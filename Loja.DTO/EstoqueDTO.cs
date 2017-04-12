
using System;

namespace Loja.DTO
{
    public class EstoqueDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCadastro{ get; set; }
    }
}
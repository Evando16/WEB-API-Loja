using System;

namespace Loja.DTO
{
    public class ItemVendaDTO
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public int ValorUnitario { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

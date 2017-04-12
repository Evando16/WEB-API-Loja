using System;
using System.Collections.Generic;

namespace Loja.DTO
{
    public class VendaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public DateTime DataCad { get; set; }
        public IEnumerable<ItemVendaDTO> ListaItens { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.ClasseLibrary
{
    [Table("Venda")]
    public class Venda
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
        public int VendedorId { get; set; }
        [ForeignKey("VendedorId")]
        public Vendedor Vendedor { get; set; }
        public DateTime DataCad { get; set; }
    }
}

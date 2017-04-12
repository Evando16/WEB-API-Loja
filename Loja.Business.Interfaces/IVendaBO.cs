using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Business.Interfaces
{
    public interface IVendaBO
    {
        VendaDTO AddVenda(VendaDTO vendaDTO);
        VendaDTO ObterVendaPorID(int idVenda);
        IEnumerable<VendaDTO> ObterVendas();
        void DeletarVenda(int idVenda);
        void AlterarVenda(VendaDTO vendaDTO);
        IEnumerable<VendaDTO> ObterVendasDoVendedor(int idVendedor);
        IEnumerable<VendaDTO> ObterVendasDoCliente(int idCliente);

        //Item venda
        void DeletarItemVenda(ItemVendaDTO itemVendaDTO);
        ItemVendaDTO AddItemVenda(ItemVendaDTO item);
    }
}
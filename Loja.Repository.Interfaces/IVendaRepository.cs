using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Repository.Interfaces
{
    public interface IVendaRepository
    {
        VendaDTO AddVenda(VendaDTO vendaDTO);
        VendaDTO ObterVendaPorID(int idVenda);
        IEnumerable<VendaDTO> ObterVendas();
        void DeletarVenda(VendaDTO vendaDTO);
        void AlterarVenda(VendaDTO vendaDTO);
        IEnumerable<VendaDTO> ObterVendasDoVendedor(int idVendedor);
        IEnumerable<VendaDTO> ObterVendasDoCliente(int idCliente);
        VendaDTO ObterVendaPorIdProduto(int idProduto);
        IEnumerable<ItemVendaDTO> ObterItensVendaPorIdVenda(int idVenda);

        //Item
        void DeletarItemVenda(ItemVendaDTO itemVendaDTO);
        ItemVendaDTO AddItemVenda(ItemVendaDTO item);
        IEnumerable<ItemVendaDTO> ObterItensVenda(int idVenda);
    }
}

using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Business.Interfaces
{
    public interface IPessoaBO
    {
        //Metodos Clientes
        ClienteDTO AddCliente(ClienteDTO clienteDTO);
        ClienteDTO ObterClienteEmail(string Email);
        ClienteDTO ObterClientePorId(int idCliente);
        void AlterarCliente(ClienteDTO clienteDTO);
        IEnumerable<ClienteDTO> ObterClientes();
        void DeletarCliente(int idCliente);

        //Metodos Vendedores
        VendedorDTO AddVendedor(VendedorDTO vendedorDTO);
        VendedorDTO ObterVendedorEmail(string Email);
        void AlterarVendedor(VendedorDTO vendedorDTO);
        IEnumerable<VendedorDTO> ObterVendedores();
        void DeletarVendedor(int idVendedor);
        VendedorDTO ObterVendedorPorId(int idVendedor);

        
    }
}

using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Repository.Interfaces
{
    public interface IPessoaRepository
    {
        //Metodos Clientes
        ClienteDTO AddCliente(ClienteDTO clienteDTO);
        ClienteDTO ObterClienteEmail(string Email);
        ClienteDTO ObterClientePorId(int idCliente);
        void AlterarCliente(ClienteDTO clienteDTO);
        IEnumerable<ClienteDTO> ObterClientes();
        void DeletarCliente(ClienteDTO clienteDTO);
        ClienteDTO ObterClientePorIdDoCliente(int idCliente);

        //Metodos Vendedores
        VendedorDTO AddVendedor(VendedorDTO vendedorDTO);
        VendedorDTO ObterVendedorEmail(string Email);
        VendedorDTO ObterVendedorPorId(int idVendedor);
        void AlterarVendedor(VendedorDTO vendedorDTO);
        IEnumerable<VendedorDTO> ObterVendedores();
        void DeletarVendedor(VendedorDTO vendedorDTO);
        VendedorDTO ObterVendedorPorIdDoVendedor(int idVendedor);

        PessoaDTO ObterPessoaPorId(int pessoaId);
        PessoaDTO ObterPessoaEmail(string Email);
        
    }
}

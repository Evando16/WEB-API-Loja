using Loja.Business.Interfaces;
using Loja.DTO;
using Loja.Repository.Interfaces;
using System.Collections.Generic;
using System;
using Loja.MyMappper;
using System.Linq;

namespace Business
{
    public class PessoaBO : IPessoaBO
    {
        private IPessoaRepository _pessoaRepository;
        private IVendaRepository _vendaRepository;

        public PessoaBO(IPessoaRepository pessoaRepository, IVendaRepository vendaRepository)
        {
            this._pessoaRepository = pessoaRepository;
            this._vendaRepository = vendaRepository;
        }

        //CLIENTE
        public ClienteDTO AddCliente(ClienteDTO clienteDTO)
        {
            try
            {
                ObterClienteEmail(clienteDTO.Email);
                return null;
            }
            catch (Exception ex)
            {
                return this._pessoaRepository.AddCliente(clienteDTO);
            }
        }

        public ClienteDTO ObterClienteEmail(string Email)
        {
            var pessoaDB = this._pessoaRepository.ObterPessoaEmail(Email);

            if (pessoaDB != null)
            {
                var clienteDB = this._pessoaRepository.ObterClientePorId(pessoaDB.Id);

                if (clienteDB != null)
                {
                    return ConverterPessoaEmClienteDTO(pessoaDB, clienteDB);
                }

                throw new Exception("Não foi encontrado cliente para o E-mail informado.");

            }

            throw new Exception("Não foi encontrado cliente para o E-mail informado.");
        }

        public void AlterarCliente(ClienteDTO clienteDTO)
        {
            var cliente = this.ObterClienteEmail(clienteDTO.Email);
            clienteDTO.IdCliente = cliente.Id;
            clienteDTO.Id = cliente.PessoaId;
            clienteDTO.PessoaId = cliente.PessoaId;
            clienteDTO.DataCadastro = cliente.DataCadastro;

            this._pessoaRepository.AlterarCliente(clienteDTO);
        }

        public IEnumerable<ClienteDTO> ObterClientes()
        {
            var listaClientes = this._pessoaRepository.ObterClientes();

            if (listaClientes.Any())
            {
                return listaClientes;
            }
            else
            {
                throw new Exception("Nenhum cliente cadastrado.");
            }
        }

        public void DeletarCliente(int idCliente)
        {
            var clienteDTO = ObterClientePorId(idCliente);

            var vendaCliente = this._vendaRepository.ObterVendasDoCliente(clienteDTO.Id);

            if (!vendaCliente.Any())
            {
                this._pessoaRepository.DeletarCliente(clienteDTO);
            }
            else
            {
                throw new Exception("Cliente não pode ser deletado, há registro de venda para o cliente.");
            }
            
        }

        //VENDEDOR
        public VendedorDTO AddVendedor(VendedorDTO vendedorDTO)
        {
            try
            {
                ObterVendedorEmail(vendedorDTO.Email);
                return null;
            }
            catch (Exception ex)
            {
                return this._pessoaRepository.AddVendedor(vendedorDTO);
            }
        }

        public VendedorDTO ObterVendedorEmail(string Email)
        {
            var pessoaDB = this._pessoaRepository.ObterPessoaEmail(Email);

            if (pessoaDB != null)
            {
                var vendedorDB = this._pessoaRepository.ObterVendedorPorId(pessoaDB.Id);

                if (vendedorDB != null)
                {
                    return ConverterPessoaEmVendedorDTO(pessoaDB, vendedorDB);
                }

                throw new Exception("Não foi encontrado vendedor para o E-mail informado.");
            }
            else
            {
                throw new Exception("Não foi encontrado vendedor para o E-mail informado.");
            }
        }

        public void AlterarVendedor(VendedorDTO vendedorDTO)
        {
            var vendedor = this.ObterVendedorEmail(vendedorDTO.Email);
            vendedorDTO.IdVendedor = vendedor.Id;
            vendedorDTO.PessoaId = vendedor.PessoaId;
            vendedorDTO.Id = vendedor.PessoaId;
            vendedorDTO.DataCadastro = vendedor.DataCadastro;

            this._pessoaRepository.AlterarVendedor(vendedorDTO);
        }

        public IEnumerable<VendedorDTO> ObterVendedores()
        {

            var listaVendedor = this._pessoaRepository.ObterVendedores();


            if (listaVendedor.Any())
            {
                return listaVendedor;
            }
            else
            {
                throw new Exception("Nenhum vendedor cadastrados.");
            }            
            
        }

        public void DeletarVendedor(int idVendedor)
        {
            var vendedorDTO = ObterVendedorPorId(idVendedor);

            var vendaVendedor = this._vendaRepository.ObterVendasDoVendedor(vendedorDTO.Id);

            if (!vendaVendedor.Any())
            {
                this._pessoaRepository.DeletarVendedor(vendedorDTO);
            }
            else
            {
                throw new Exception("Vendedor não pode ser deletado, há registro de venda para o vendedor.");
            }
        }

        //PESSOA
        public PessoaDTO ObterPessoaPorId(int idPessoa)
        {
            var pessoaDTO = this._pessoaRepository.ObterPessoaPorId(idPessoa);

            if (pessoaDTO != null)
            {
                return pessoaDTO;
            }
            else
            {
                throw new Exception("Pessoa não foi encontrada.");
            }
        }

        public VendedorDTO ObterVendedorPorId(int idVendedor)
        {
            var vendedorDTO = this._pessoaRepository.ObterVendedorPorIdDoVendedor(idVendedor);

            if (vendedorDTO != null)
            {
                return vendedorDTO;
            }
            else
            {
                throw new Exception("Vendedor não encontrado");
            }
        }

        public ClienteDTO ObterClientePorId(int idCliente)
        {
            var clienteDTO = this._pessoaRepository.ObterClientePorIdDoCliente(idCliente);

            if (clienteDTO != null)
            {
                return clienteDTO;
            }
            else
            {
                throw new Exception("Cliente não encontrado.");
            }
        }


        private ClienteDTO ConverterPessoaEmClienteDTO(PessoaDTO pessoaDB, ClienteDTO clienteDTO)
        {
            var cliente = AutoMapperConfig.MapTo<ClienteDTO>(pessoaDB);
            cliente.IdCliente = clienteDTO.Id;
            cliente.PessoaId = pessoaDB.Id;

            return cliente;
        }

        private VendedorDTO ConverterPessoaEmVendedorDTO(PessoaDTO pessoaDB, VendedorDTO vendedorDTO)
        {
            var vendedor = AutoMapperConfig.MapTo<VendedorDTO>(pessoaDB);
            vendedor.IdVendedor = vendedorDTO.Id;
            vendedor.Matricula = vendedorDTO.Matricula;
            vendedor.PessoaId = pessoaDB.Id;

            return vendedor;
        }
    }
}

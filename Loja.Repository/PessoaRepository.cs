using Loja.ClasseLibrary;
using Loja.DTO;
using Loja.MyMappper;
using Loja.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Loja.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        public PessoaDTO AddPessoa(PessoaDTO pessoaDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                //Pessoa pessoa = new Pessoa
                //{
                //    Nome = pessoaDTO.Nome,
                //    Email = pessoaDTO.Email,
                //    Endereco = pessoaDTO.Endereco,
                //    Senha = pessoaDTO.Senha,
                //    Telefone = pessoaDTO.Telefone,
                //    Tipo = (TipoPessoa)pessoaDTO.Tipo
                //};

                var pessoaDB = context.Pessoas.Add(AutoMapperConfig.MapTo<Pessoa>(pessoaDTO));
                pessoaDB.DataCadastro = DateTime.Now;
                context.SaveChanges();

                return AutoMapperConfig.MapTo<PessoaDTO>(pessoaDB);
            }
        }

        public void AlterarPessoa(PessoaDTO pessoaDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                context.Pessoas.AddOrUpdate(AutoMapperConfig.MapTo<Pessoa>(pessoaDTO));
                context.SaveChanges();
            }
        }

        public void DeletarPessoa(PessoaDTO pessoaDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                Pessoa pessoa = AutoMapperConfig.MapTo<Pessoa>(pessoaDTO);
                context.Entry(pessoa).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public PessoaDTO ObterPessoaEmail(string Email)
        {
            using (var context = new RepositoryLojaContext())
            {
                var pessoaDB = context.Pessoas.SingleOrDefault(p => p.Email.Equals(Email));

                return AutoMapperConfig.MapTo<PessoaDTO>(pessoaDB);
            }
        }

        public IEnumerable<PessoaDTO> ObterPessoas()
        {
            using (var context = new RepositoryLojaContext())
            {
                var pessoaDB = context.Pessoas.ToList();

                return AutoMapperConfig.MapTo<IEnumerable<PessoaDTO>>(pessoaDB);
            }
        }

        public PessoaDTO ObterPessoaPorId(int idPessoa)
        {
            using (var context = new RepositoryLojaContext())
            {
                var pessoaDB = context.Pessoas.SingleOrDefault(p => p.Id == idPessoa);

                return AutoMapperConfig.MapTo<PessoaDTO>(pessoaDB);
            }
        }

        public ClienteDTO AddCliente(ClienteDTO clienteDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                var pessoaDB = AddPessoa(AutoMapperConfig.MapTo<PessoaDTO>(clienteDTO));

                clienteDTO.PessoaId = pessoaDB.Id;
                context.Clientes.AddOrUpdate(AutoMapperConfig.MapTo<Cliente>(clienteDTO));
                context.SaveChanges();

                var cliente = ObterClientePorId(pessoaDB.Id);
                clienteDTO.Id = pessoaDB.Id;
                clienteDTO.IdCliente = cliente.Id;

                return clienteDTO;
            }
        }

        public VendedorDTO AddVendedor(VendedorDTO vendedorDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                var pessoaDB = AddPessoa(AutoMapperConfig.MapTo<PessoaDTO>(vendedorDTO));

                vendedorDTO.PessoaId = pessoaDB.Id;
                context.Vendedores.AddOrUpdate(AutoMapperConfig.MapTo<Vendedor>(vendedorDTO));
                context.SaveChanges();

                var vendedor = ObterVendedorPorId(pessoaDB.Id);
                vendedorDTO.Id = pessoaDB.Id;
                vendedorDTO.IdVendedor = vendedor.Id;

                return vendedorDTO;
            }
        }

        public void AlterarCliente(ClienteDTO clienteDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                AlterarPessoa(AutoMapperConfig.MapTo<PessoaDTO>(clienteDTO));

                var cliente = AutoMapperConfig.MapTo<Cliente>(ObterClientePorId(clienteDTO.Id));
                context.Clientes.AddOrUpdate(cliente);
                context.SaveChanges();
            }
        }

        public void AlterarVendedor(VendedorDTO vendedorDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                AlterarPessoa(AutoMapperConfig.MapTo<PessoaDTO>(vendedorDTO));

                var vendedor = AutoMapperConfig.MapTo<Vendedor>(ObterVendedorPorId(vendedorDTO.Id));
                
                //Faz as alterações que sejam necessárias

                context.Vendedores.AddOrUpdate(vendedor);
                context.SaveChanges();
            }
        }

        public void DeletarCliente(ClienteDTO clienteDTO)
        {
            using (var context = new RepositoryLojaContext())
            {

                Cliente cliente = AutoMapperConfig.MapTo<Cliente>(clienteDTO);
                context.Entry(cliente).State = EntityState.Deleted;
                context.SaveChanges();

                DeletarPessoa(ObterPessoaPorId(cliente.PessoaId));
            }
        }

        public void DeletarVendedor(VendedorDTO vendedorDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                Vendedor vendedor = AutoMapperConfig.MapTo<Vendedor>(vendedorDTO);
                context.Entry(vendedor).State = EntityState.Deleted;
                context.SaveChanges();

                //DeletarPessoa(ObterPessoaPorId(vendedorDTO.PessoaId));
                DeletarPessoa(ObterPessoaPorId(vendedorDTO.PessoaId));
            }
        }

        public ClienteDTO ObterClienteEmail(string Email)
        {
            using (var context = new RepositoryLojaContext())
            {
                var pessoaDB = ObterPessoaEmail(Email);
                var clienteDB = context.Clientes.SingleOrDefault(c => c.PessoaId == pessoaDB.Id);

                var clienteDTO = ConverterPessoaEmClienteDTO(pessoaDB, clienteDB);

                return clienteDTO;
            }
        }

        private ClienteDTO ConverterPessoaEmClienteDTO(PessoaDTO pessoaDB, Cliente clienteDB)
        {
            var clienteDTO = AutoMapperConfig.MapTo<ClienteDTO>(pessoaDB);
            clienteDTO.IdCliente = clienteDB.Id;
            clienteDTO.PessoaId = pessoaDB.Id;
            return clienteDTO;
        }

        private VendedorDTO ConverterPessoaEmVendedorDTO(PessoaDTO pessoaDB, Vendedor vendedorDB)
        {
            var vendedorDTO = AutoMapperConfig.MapTo<VendedorDTO>(pessoaDB);
            vendedorDTO.IdVendedor = vendedorDB.Id;
            vendedorDTO.Matricula = vendedorDB.Matricula;
            vendedorDTO.PessoaId = pessoaDB.Id;

            return vendedorDTO;
        }

        public IEnumerable<ClienteDTO> ObterClientes()
        {
            using (var context = new RepositoryLojaContext())
            {
                var listaClientes = context.Clientes.ToList();

                List<ClienteDTO> listaClienteDTO = new List<ClienteDTO>();
                foreach (var cliente in listaClientes)
                {
                    var pessoaDB = ObterPessoaPorId(cliente.PessoaId);

                    listaClienteDTO.Add(ConverterPessoaEmClienteDTO(pessoaDB, cliente));
                }

                return listaClienteDTO;
            }
        }


        public VendedorDTO ObterVendedorEmail(string Email)
        {
            using (var context = new RepositoryLojaContext())
            {
                var pessoaDB = ObterPessoaEmail(Email);

                if (pessoaDB != null)
                {
                    var vendedorDB = context.Vendedores.SingleOrDefault(v => v.PessoaId == pessoaDB.Id);
                    var vendedorDTO = ConverterPessoaEmVendedorDTO(pessoaDB, vendedorDB);

                    return vendedorDTO;
                }

                return null;
            }
        }

        public IEnumerable<VendedorDTO> ObterVendedores()
        {
            using (var context = new RepositoryLojaContext())
            {
                var listaVendedores = context.Vendedores.ToList();

                List<VendedorDTO> listaVendedorDTO = new List<VendedorDTO>();
                foreach (var vendedor in listaVendedores)
                {
                    var pessoaDB = ObterPessoaPorId(vendedor.PessoaId);
                    listaVendedorDTO.Add(ConverterPessoaEmVendedorDTO(pessoaDB, vendedor));
                }

                return listaVendedorDTO;
            }
        }

        public ClienteDTO ObterClientePorId(int idCliente)
        {
            using (var context = new RepositoryLojaContext())
            {
                var clienteDB = context.Clientes.SingleOrDefault(c => c.PessoaId == idCliente);

                return AutoMapperConfig.MapTo<ClienteDTO>(clienteDB);
            }
        }

        public VendedorDTO ObterVendedorPorId(int idVendedor)
        {
            using (var context = new RepositoryLojaContext())
            {
                var vendedorDB = context.Vendedores.SingleOrDefault(v => v.PessoaId == idVendedor);

                return AutoMapperConfig.MapTo<VendedorDTO>(vendedorDB);
            }
        }

        public ClienteDTO ObterClientePorIdDoCliente(int idCliente)
        {
            using (var context = new RepositoryLojaContext())
            {
                var clienteDB = context.Clientes.SingleOrDefault(c => c.Id == idCliente);

                return AutoMapperConfig.MapTo<ClienteDTO>(clienteDB);
            }
        }

        public VendedorDTO ObterVendedorPorIdDoVendedor(int idVendedor)
        {
            using (var context = new RepositoryLojaContext())
            {
                var vendedorDB = context.Vendedores.SingleOrDefault(v => v.Id == idVendedor);

                return AutoMapperConfig.MapTo<VendedorDTO>(vendedorDB);
            }
        }

    }
}
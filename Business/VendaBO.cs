using System;
using System.Collections.Generic;
using Loja.Business.Interfaces;
using Loja.DTO;
using Loja.Repository.Interfaces;
using System.Linq;

namespace Business
{
    public class VendaBO : IVendaBO
    {
        private IVendaRepository _vendaRepository;
        private IEstoqueRepository _estoqueRepository;
        private IPessoaRepository _pessoaRepository;

        private IProdutosRepository _produtoRepository;

        public VendaBO(IVendaRepository vendaRepository, IEstoqueRepository estoqueRepository,
            IProdutosRepository produtoRepository, IPessoaRepository pessoaRepository)
        {
            this._vendaRepository = vendaRepository;
            this._estoqueRepository = estoqueRepository;
            this._produtoRepository = produtoRepository;
            this._pessoaRepository = pessoaRepository;
        }

        public VendaDTO AddVenda(VendaDTO vendaDTO)
        {
            var cliente = this._pessoaRepository.ObterClientePorIdDoCliente(vendaDTO.ClienteId);

            if (cliente == null)
            {
                throw new Exception("Cliente não cadastrado.");
            }

            var vendedor = this._pessoaRepository.ObterVendedorPorIdDoVendedor(vendaDTO.VendedorId);

            if (vendedor == null)
            {
                throw new Exception("Vendedor não cadastrado.");
            }

            foreach (var item in vendaDTO.ListaItens)
            {
                var produto = this._produtoRepository.ObterProdutoID(item.ProdutoId);

                if (produto == null)
                {
                    throw new Exception("Produto não encontrado.");
                }

                var estoque = ObterEstoqueIdProduto(item.ProdutoId);
                
                if (estoque.Quantidade <= 0)
                {                    
                    throw new Exception("Não há estoque para o produto " + produto.Nome);
                }
                else if (estoque.Quantidade - item.Quantidade < 0)
                {
                    throw new Exception("Venda não pode ser realizada pois não há quantidade suficiente do produto " + produto.Nome);
                }
            }

            foreach (var item in vendaDTO.ListaItens)
            {
                var estoque = ObterEstoqueIdProduto(item.ProdutoId);

                estoque.Quantidade = item.Quantidade;
                this._estoqueRepository.DecrementarEstoque(estoque);
            }   
            return this._vendaRepository.AddVenda(vendaDTO);
        }

        //Fazer com que a alteração de venda também delete itens da venda no banco de dados caso seja necessario
        public void AlterarVenda(VendaDTO vendaDTO)
        {
            var vendedor = this._pessoaRepository.ObterVendedorPorIdDoVendedor(vendaDTO.VendedorId);

            if (vendedor == null)
            {
                throw new Exception("Vendedor não cadastrado.");
            }

            var venda = this._vendaRepository.ObterVendaPorID(vendaDTO.Id);

            if (venda != null)
            {
                vendaDTO.DataCad = venda.DataCad;
                vendaDTO.ClienteId = venda.ClienteId;
                this._vendaRepository.AlterarVenda(vendaDTO);
            }
            else
            {
                throw new Exception("Não foi encontrada uma venda para o ID informado");
            }            
        }

        public void DeletarVenda(int idVenda)
        {
            var vendaDTO = this._vendaRepository.ObterVendaPorID(idVenda);

            if (vendaDTO != null)
            {

                vendaDTO.ListaItens = this._vendaRepository.ObterItensVendaPorIdVenda(idVenda);

                foreach (var item in vendaDTO.ListaItens)
                {
                    var estoque = ObterEstoqueIdProduto(item.ProdutoId);

                    estoque.Quantidade = item.Quantidade;
                    this._estoqueRepository.IncrementarEstoque(estoque);

                    this._vendaRepository.DeletarItemVenda(item);
                }

                this._vendaRepository.DeletarVenda(vendaDTO);
            }
            else
            {
                throw new Exception("Venda não encontrada.");
            }           
        }

        public EstoqueDTO ObterEstoqueIdProduto(int idProduto)
        {
            var estoque = this._estoqueRepository.ObterEstoqueIdProduto(idProduto);

            if (estoque != null)
            {
                return estoque;
            }
            else
            {
                throw new Exception("Sem estoque para o produto.");
            }
        }

        public VendaDTO ObterVendaPorID(int idVenda)
        {
            var venda = this._vendaRepository.ObterVendaPorID(idVenda);

            if (venda != null)
            {
                return venda;
            }
            else
            {
                throw new Exception("Venda não encontrada.");
            }
        }

        public IEnumerable<VendaDTO> ObterVendas()
        {
            return this._vendaRepository.ObterVendas();
        }

        public IEnumerable<VendaDTO> ObterVendasDoCliente(int idCliente)
        {
            var vendaCliente = this._vendaRepository.ObterVendasDoCliente(idCliente);

            if (vendaCliente != null)
            {
                return vendaCliente;
            }
            else
            {
                throw new Exception("Não foi encontrada venda para este cliente.");
            }
        }

        public IEnumerable<VendaDTO> ObterVendasDoVendedor(int idVendedor)
        {
            var vendaVendedor = this._vendaRepository.ObterVendasDoVendedor(idVendedor);

            if (vendaVendedor != null)
            {
                return vendaVendedor;
            }
            else
            {
                throw new Exception("Não foi encontrada venda para este vendedor.");
            }
        }

        public void DeletarItemVenda(ItemVendaDTO itemVendaDTO)
        {
            var venda = ObterVendaPorID(itemVendaDTO.VendaId);

            venda.ListaItens = this._vendaRepository.ObterItensVendaPorIdVenda(venda.Id);

            var itemVenda = venda.ListaItens.SingleOrDefault(i => i.ProdutoId == itemVendaDTO.ProdutoId);

            if (itemVenda != null)
            {
                var estoque = this._estoqueRepository.ObterEstoqueIdProduto(itemVenda.ProdutoId);
                estoque.Quantidade = itemVenda.Quantidade;
                this._estoqueRepository.IncrementarEstoque(estoque);

                this._vendaRepository.DeletarItemVenda(itemVenda);
            }
            else
            {
                throw new Exception("Item não encontrado.");
            }
        }

        public ItemVendaDTO AddItemVenda(ItemVendaDTO itemVendaDTO)
        {
            var venda = ObterVendaPorID(itemVendaDTO.VendaId);

            venda.ListaItens = this._vendaRepository.ObterItensVendaPorIdVenda(venda.Id);

            var item = venda.ListaItens.SingleOrDefault(i => i.ProdutoId == itemVendaDTO.ProdutoId);

            if (item == null)
            {
                var estoque = ObterEstoqueIdProduto(itemVendaDTO.ProdutoId);

                estoque.Quantidade = itemVendaDTO.Quantidade;
                this._estoqueRepository.DecrementarEstoque(estoque);

                return this._vendaRepository.AddItemVenda(itemVendaDTO);
            }
            else
            {
                throw new Exception("O produto já está cadastrado na venda;");
            }            
        }
    }
}

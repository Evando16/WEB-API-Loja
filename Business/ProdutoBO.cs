using System;
using System.Collections.Generic;
using Loja.Business.Interfaces;
using Loja.DTO;
using Loja.Repository.Interfaces;
using System.Linq;

namespace Business
{
    public class ProdutoBO : IProdutoBO
    {
        private IProdutosRepository _produtoRepository;
        private IFornecedorRepository _fornecedorRepository;
        private IVendaRepository _vendaRepository;

        public ProdutoBO(IProdutosRepository produtoRepository, IFornecedorRepository fornecedorRepository,
            IVendaRepository vendaRepository)
        {
            this._produtoRepository = produtoRepository;
            this._fornecedorRepository = fornecedorRepository;
            this._vendaRepository = vendaRepository;
        }

        public ProdutoDTO AddProduto(ProdutoDTO produtoDTO)
        {
            var fornecedor = _fornecedorRepository.ObterFornecedorID(produtoDTO.FornecedorId);

            if (fornecedor != null)
            {
                var produtoDB = ObterProdutoPorNomeEFornecedor(produtoDTO.Nome, produtoDTO.FornecedorId);

                if (produtoDB == null)
                {
                    return this._produtoRepository.AddProduto(produtoDTO);
                }
                else
                {
                    throw new Exception("Produto já está cadastrado.");
                }
            }
            else
            {
                throw new Exception("Este fornecedor não está cadastrado.");
            }
        }

        public void AlterarProduto(ProdutoDTO produtoDTO)
        {
            var produto = ObterProdutoPorNomeEFornecedor(produtoDTO.Nome, produtoDTO.FornecedorId);

            if (produto != null)
            {
                produtoDTO.Id = produto.Id;
                produtoDTO.DataCadastro = produto.DataCadastro;
                this._produtoRepository.AlterarProduto(produtoDTO);
            }
            else
            {
                throw new Exception("Produto não está cadastrado.");
            }
        }

        public void DeletarProduto(int idProduto)
        {
            var produtoDTO = ObterProdutoID(idProduto);

            var venda = this._vendaRepository.ObterVendaPorIdProduto(idProduto);

            if (venda == null)
            {
                this._produtoRepository.DeletarProduto(produtoDTO);
            }
            else
            {
                throw new Exception("Produto não pode ser excluido porque há registo de venda com este produto.");
            }            
        }

        //Comparation don't is working with Contains
        public IEnumerable<ProdutoDTO> ObterProduto(string Nome = null)
        {
            var listaProduto = this._produtoRepository.ObterProduto(Nome);

            if (listaProduto.Any())
            {
                return listaProduto;
            }
            else
            {
                throw new Exception("Nenhum produto cadastrados.");
            }
        }

        public ProdutoDTO ObterProdutoID(int idProduto)
        {
            var produtoDTO = this._produtoRepository.ObterProdutoID(idProduto);

            if (produtoDTO != null)
            {
                return produtoDTO;
            }
            else
            {
                throw new Exception("Produto não foi encontrado.");
            }
        }

        public IEnumerable<ProdutoDTO> ObterProdutoIdFornecedor(int idFornecedor)
        {
            var listaProduto = this._produtoRepository.ObterProdutoIdFornecedor(idFornecedor);

            if (listaProduto.Any())
            {
                return listaProduto;
            }
            else
            {
                throw new Exception("Nenhum produto cadastrados para o fornecedor informado.");
            }
        }

        private ProdutoDTO ObterProdutoPorNomeEFornecedor(string Nome, int idFornecedor)
        {
             return this._produtoRepository.ObterProdutoPorNomeEFornecedor(Nome, idFornecedor);
        }
    }
}
using System;
using System.Collections.Generic;
using Loja.Business.Interfaces;
using Loja.DTO;
using Loja.Repository.Interfaces;

namespace Business
{
    public class EstoqueBO : IEstoqueBO
    {
        private IEstoqueRepository _estoqueRepository;
        private IProdutosRepository _produtoRepository;        

        public EstoqueBO(IEstoqueRepository estoqueRepository, IProdutosRepository produtoRepository)
        {
            this._estoqueRepository = estoqueRepository;
            this._produtoRepository = produtoRepository;            
        }

        public EstoqueDTO AddEstoque(EstoqueDTO estoqueDTO)
        {
            BuscarProdutoId(estoqueDTO.ProdutoId);

            EstoqueDTO estoque = this._estoqueRepository.ObterEstoqueIdProduto(estoqueDTO.ProdutoId);
            if (estoque != null)
            {
                estoqueDTO.Id = estoque.Id;
                estoqueDTO.Quantidade += estoque.Quantidade;
                estoqueDTO.DataCadastro = estoque.DataCadastro;
            }
            else
            {
                estoqueDTO.DataCadastro = DateTime.Now;
            }


            return this._estoqueRepository.AddEstoque(estoqueDTO);
        }

        public void AlterarEstoque(EstoqueDTO estoqueDTO)
        {
            BuscarProdutoId(estoqueDTO.ProdutoId);

            var estoque = ObterEstoqueIdProduto(estoqueDTO.ProdutoId);

            estoqueDTO.DataCadastro = estoque.DataCadastro;
            this._estoqueRepository.AlterarEstoque(estoqueDTO);
        }

        public void DeletarEstoque(int idEstoque)
        {
            var estoqueDTO = ObterEstoqueId(idEstoque);

            this._estoqueRepository.DeletarEstoque(estoqueDTO);

        }

        public IEnumerable<EstoqueDTO> ObterEstoqueNomeProduto(string nome = null)
        {
            var estoque = this._estoqueRepository.ObterEstoqueNomeProduto(nome);

            if (estoque != null)
            {
                return estoque;
            }
            else
            {
                throw new Exception("Estoque não encontrado.");
            }
        }

        public EstoqueDTO ObterEstoqueId(int idEstoque)
        {
            var estoqueDTO = this._estoqueRepository.ObterEstoqueId(idEstoque);
            if (estoqueDTO != null)
            {
                return estoqueDTO;
            }
            else
            {
                throw new Exception("Estoque não foi encontrado.");
            }
        }

        public EstoqueDTO ObterEstoqueIdProduto(int idProduto)
        {
            var estoqueDTO = this._estoqueRepository.ObterEstoqueIdProduto(idProduto);
            if (estoqueDTO != null)
            {
                return estoqueDTO;
            }
            else
            {
                throw new Exception("Estoque não encontrado.");
            }
        }

        public EstoqueDTO IncrementarEstoque(EstoqueDTO estoqueDTO)
        {
            var estoque = ObterEstoqueIdProduto(estoqueDTO.ProdutoId);            

            return this._estoqueRepository.IncrementarEstoque(estoqueDTO);
        }

        public EstoqueDTO DecrementarEstoque(EstoqueDTO estoqueDTO)
        {
            var estoque = ObterEstoqueIdProduto(estoqueDTO.ProdutoId);

            if (estoque.Quantidade - estoqueDTO.Quantidade < 0)
            {
                throw new Exception("Estoque não pode ficar negativo.");
            }
            return this._estoqueRepository.DecrementarEstoque(estoqueDTO);
        }

        //Produto
        private ProdutoDTO BuscarProdutoId(int produtoId)
        {
            var produto = this._produtoRepository.ObterProdutoID(produtoId);

            if (produto == null)
            {
                throw new Exception("Só é possível adicionar um estoque de um produto cadastrado.");
            }

            return produto;
        }
    }
}

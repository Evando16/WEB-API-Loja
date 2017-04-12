using System.Collections.Generic;
using Loja.DTO;
using Loja.Repository.Interfaces;
using Loja.MyMappper;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using Loja.ClasseLibrary;
using System;

namespace Loja.Repository
{
    public class ProdutoRepository : IProdutosRepository
    {
        public ProdutoDTO AddProduto(ProdutoDTO produtoDTO)
        {
            using (var context = new RepositoryLojaContext())
            {

                var produtoDB = context.Produtos.Add(AutoMapperConfig.MapTo<Produto>(produtoDTO));
                produtoDB.DataCadastro = DateTime.Now;
                context.SaveChanges();

                return AutoMapperConfig.MapTo<ProdutoDTO>(produtoDB);
            }
        }

        public void AlterarProduto(ProdutoDTO produtoDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                context.Produtos.AddOrUpdate(AutoMapperConfig.MapTo<Produto>(produtoDTO));
                context.SaveChanges();
            }
        }

        public void DeletarProduto(ProdutoDTO produtoDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                Produto produto = AutoMapperConfig.MapTo<Produto>(produtoDTO);
                context.Entry(produto).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IEnumerable<ProdutoDTO> ObterProduto(string Nome = null)
        {
            using (var context = new RepositoryLojaContext())
            {
                if (Nome != null)
                {
                    var produtoDB = context.Produtos.Where(p => p.Nome.Equals(Nome, StringComparison.CurrentCultureIgnoreCase));
                    return AutoMapperConfig.MapTo<IEnumerable<ProdutoDTO>>(produtoDB);
                }
                else
                {
                    var produtoDB = context.Produtos.ToList();
                    return AutoMapperConfig.MapTo<IEnumerable<ProdutoDTO>>(produtoDB);
                }
            }
        }

        public ProdutoDTO ObterProdutoID(int idProduto)
        {
            using (var context = new RepositoryLojaContext())
            {
                var produtoDB = context.Produtos.SingleOrDefault(p => p.Id == idProduto);
                return AutoMapperConfig.MapTo<ProdutoDTO>(produtoDB);
            }

        }

        public IEnumerable<ProdutoDTO> ObterProdutoIdFornecedor(int idFornecedor)
        {
            using (var context = new RepositoryLojaContext())
            {
                var produtoDB = context.Produtos.Where(p => p.FornecedorId == idFornecedor);
                return AutoMapperConfig.MapTo<IEnumerable<ProdutoDTO>>(produtoDB);
            }

        }

        public ProdutoDTO ObterProdutoPorNomeEFornecedor(string Nome, int idFornecedor)
        {
            using (var context = new RepositoryLojaContext())
            {
                var produtoDB = context.Produtos.SingleOrDefault(
                    p => p.Nome.Equals(Nome, StringComparison.CurrentCultureIgnoreCase)
                    && p.FornecedorId == idFornecedor);

                    return AutoMapperConfig.MapTo<ProdutoDTO>(produtoDB);
            }
        }
    }
}

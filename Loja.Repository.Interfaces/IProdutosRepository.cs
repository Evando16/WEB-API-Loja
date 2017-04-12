using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Repository.Interfaces
{
    public interface IProdutosRepository
    {
        ProdutoDTO AddProduto(ProdutoDTO produtoDTO);
        ProdutoDTO ObterProdutoID(int idProduto);
        IEnumerable<ProdutoDTO> ObterProduto(string Nome = null);
        ProdutoDTO ObterProdutoPorNomeEFornecedor(string Nome, int idFornecedor);
        void AlterarProduto(ProdutoDTO produtoDTO);
        void DeletarProduto(ProdutoDTO produtoDTO);
        IEnumerable<ProdutoDTO> ObterProdutoIdFornecedor(int idFornecedor);
    }
}

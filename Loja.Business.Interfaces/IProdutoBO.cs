using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Business.Interfaces
{
    public interface IProdutoBO
    {
        ProdutoDTO AddProduto(ProdutoDTO produtoDTO);
        ProdutoDTO ObterProdutoID(int idProduto);
        IEnumerable<ProdutoDTO> ObterProduto(string Nome = null);
        void AlterarProduto(ProdutoDTO produtoDTO);
        void DeletarProduto(int idProduto);
        IEnumerable<ProdutoDTO> ObterProdutoIdFornecedor(int idFornecedor);
    }
}

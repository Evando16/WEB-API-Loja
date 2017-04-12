using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Business.Interfaces
{
    public interface IEstoqueBO
    {
        EstoqueDTO AddEstoque(EstoqueDTO estoqueDTO);
        EstoqueDTO ObterEstoqueId(int idEstoque);
        EstoqueDTO ObterEstoqueIdProduto(int idProduto);
        IEnumerable<EstoqueDTO> ObterEstoqueNomeProduto(string nome = null);
        void AlterarEstoque(EstoqueDTO estoqueDTO);
        void DeletarEstoque(int idEstoque);

        EstoqueDTO IncrementarEstoque(EstoqueDTO estoqueDTO);
        EstoqueDTO DecrementarEstoque(EstoqueDTO estoqueDTO);
    }
}

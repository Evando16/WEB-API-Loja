using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Repository.Interfaces
{
    public interface IEstoqueRepository
    {
        EstoqueDTO AddEstoque(EstoqueDTO estoqueDTO);
        EstoqueDTO ObterEstoqueId(int idEstoque);
        EstoqueDTO ObterEstoqueIdProduto(int idProduto);
        IEnumerable<EstoqueDTO> ObterEstoqueNomeProduto(string nome = null);
        void AlterarEstoque(EstoqueDTO estoqueDTO);
        void DeletarEstoque(EstoqueDTO estoqueDTO);
        EstoqueDTO IncrementarEstoque(EstoqueDTO estoqueDTO);
        EstoqueDTO DecrementarEstoque(EstoqueDTO estoqueDTO);
    }
}

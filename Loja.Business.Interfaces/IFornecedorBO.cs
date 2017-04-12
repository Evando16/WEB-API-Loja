using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Business.Interfaces
{
    public interface IFornecedorBO
    {
        FornecedorDTO AddFornecedor(FornecedorDTO fornecedorDTO);
        FornecedorDTO ObterFornecedorEmail(string email);
        FornecedorDTO ObterFornecedorID(int idFornecedor);
        void AlterarFornecedor(FornecedorDTO fornecedorDTO);
        IEnumerable<FornecedorDTO> ObterFornecedores();
        void DeletarFornecedor(int idFornecedor);
    }
}

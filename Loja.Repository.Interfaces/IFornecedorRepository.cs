using Loja.DTO;
using System.Collections.Generic;

namespace Loja.Repository.Interfaces
{
    public interface IFornecedorRepository
    {
        FornecedorDTO AddFornecedor(FornecedorDTO fornecedorDTO);
        FornecedorDTO ObterFornecedorEmail(string email);
        FornecedorDTO ObterFornecedorID(int idFornecedor);
        IEnumerable<FornecedorDTO> ObterFornecedores();
        void AlterarFornecedor(FornecedorDTO fornecedorDTO);
        void DeletarFornecedor(FornecedorDTO fornecedorDTO);
    }
}

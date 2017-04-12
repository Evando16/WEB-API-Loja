using System;
using System.Collections.Generic;
using Loja.Business.Interfaces;
using Loja.DTO;
using Loja.Repository.Interfaces;
using System.Linq;

namespace Business
{
    public class FornecedorBO : IFornecedorBO
    {
        private IFornecedorRepository _fornecedorRepository;
        private IProdutosRepository _produtoRepository;

        public FornecedorBO(IFornecedorRepository fornecedorRepository, IProdutosRepository produtoRepository)
        {
            this._fornecedorRepository = fornecedorRepository;
            this._produtoRepository = produtoRepository;
        }

        public FornecedorDTO AddFornecedor(FornecedorDTO fornecedorDTO)
        {
            try
            {
                ObterFornecedorEmail(fornecedorDTO.Email);
                return null;
            }
            catch (Exception ex)
            {
                return this._fornecedorRepository.AddFornecedor(fornecedorDTO);
            }      
        }

        public void AlterarFornecedor(FornecedorDTO fornecedorDTO)
        {
            var fornecedor = ObterFornecedorEmail(fornecedorDTO.Email);
            fornecedorDTO.DataCadastro = fornecedor.DataCadastro;
            fornecedorDTO.Id = fornecedor.Id;

            this._fornecedorRepository.AlterarFornecedor(fornecedorDTO);
        }

        public void DeletarFornecedor(int idFornecedor)
        {
            var fornecedorDTO = ObterFornecedorID(idFornecedor);

            var listaDependencia = _produtoRepository.ObterProdutoIdFornecedor(idFornecedor);
            
            if (!listaDependencia.Any())
            {
                this._fornecedorRepository.DeletarFornecedor(fornecedorDTO);
            }
            else
            {
                throw new Exception("Fornecedor não pode ser apagado, há registrados para este fornecedor.");
            }
        }

        public IEnumerable<FornecedorDTO> ObterFornecedores()
        {

            var listaFornecedores = this._fornecedorRepository.ObterFornecedores();

            if (listaFornecedores != null)
            {
                return listaFornecedores;
            }
            else
            {
                throw new Exception("Nenhum fornecedor cadastrado.");
            }
        }

        public FornecedorDTO ObterFornecedorEmail(string email)
        {
            var fornecedorDB = this._fornecedorRepository.ObterFornecedorEmail(email);

            if (fornecedorDB != null)
            {
                return fornecedorDB;
            }

            throw new Exception("Não foi encontrado fornecedor para o E-mail informado.");                        
        }

        public FornecedorDTO ObterFornecedorID(int idFornecedor)
        {
            var fornecedorDTO = this._fornecedorRepository.ObterFornecedorID(idFornecedor);

            if (fornecedorDTO != null)
            {
                return fornecedorDTO;
            }
            else
            {
                throw new Exception("Fornecedor não foi encontrado");
            }
        }
    }
}
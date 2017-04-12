using Loja.DTO;
using Loja.Repository.Interfaces;
using Loja.MyMappper;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using Loja.ClasseLibrary;
using System;

namespace Loja.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        public FornecedorDTO AddFornecedor(FornecedorDTO fornecedorDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                var fornecedorDB = context.Fornecedores.Add(AutoMapperConfig.MapTo<Fornecedor>(fornecedorDTO));
                fornecedorDB.DataCadastro = DateTime.Now;
                context.SaveChanges();

                return AutoMapperConfig.MapTo<FornecedorDTO>(fornecedorDB);
            }
        }

        public void AlterarFornecedor(FornecedorDTO fornecedorDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                context.Fornecedores.AddOrUpdate(AutoMapperConfig.MapTo<Fornecedor>(fornecedorDTO));
                context.SaveChanges();
            }
        }

        public void DeletarFornecedor(FornecedorDTO fornecedorDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                Fornecedor fornecedor = AutoMapperConfig.MapTo<Fornecedor>(fornecedorDTO);
                context.Entry(fornecedor).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IEnumerable<FornecedorDTO> ObterFornecedores()
        {
            using (var context = new RepositoryLojaContext())
            {
                var fornecedoresDB = context.Fornecedores.ToList();

                return AutoMapperConfig.MapTo<IEnumerable<FornecedorDTO>>(fornecedoresDB);
            }
        }

        public FornecedorDTO ObterFornecedorEmail(string email)
        {
            using (var context = new RepositoryLojaContext())
            {
                var fornecedorDB = context.Fornecedores.SingleOrDefault(f => f.Email.Equals(email));
                return AutoMapperConfig.MapTo<FornecedorDTO>(fornecedorDB);
            }
        }

        public FornecedorDTO ObterFornecedorID(int idFornecedor)
        {
            using (var context = new RepositoryLojaContext())
            {
                var fornecedorDB = context.Fornecedores.SingleOrDefault(f => f.Id == idFornecedor);
                return AutoMapperConfig.MapTo<FornecedorDTO>(fornecedorDB);
            }
        }
    }
}

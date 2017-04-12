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
    public class EstoqueRepository : IEstoqueRepository
    {
        public EstoqueDTO AddEstoque(EstoqueDTO estoqueDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                context.Estoques.AddOrUpdate(AutoMapperConfig.MapTo<Estoque>(estoqueDTO));               
                context.SaveChanges();

                var estoqueDB = ObterEstoqueIdProduto(estoqueDTO.ProdutoId);

                return AutoMapperConfig.MapTo<EstoqueDTO>(estoqueDB);
            }
        }

        public void AlterarEstoque(EstoqueDTO estoqueDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                context.Estoques.AddOrUpdate(AutoMapperConfig.MapTo<Estoque>(estoqueDTO));
                context.SaveChanges();
            }
        }

        public void DeletarEstoque(EstoqueDTO estoqueDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                Estoque estoque = AutoMapperConfig.MapTo<Estoque>(estoqueDTO);
                context.Entry(estoque).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public EstoqueDTO ObterEstoqueId(int idEstoque)
        {
            using (var context = new RepositoryLojaContext())
            {
                var estoqueDB = context.Estoques.SingleOrDefault( e => e.Id == idEstoque);
                return AutoMapperConfig.MapTo<EstoqueDTO>(estoqueDB);
            }
        }

        public EstoqueDTO ObterEstoqueIdProduto(int idProduto)
        {
            using (var context = new RepositoryLojaContext())
            {
                var estoqueDB = context.Estoques.SingleOrDefault(e => e.ProdutoId == idProduto);
                return AutoMapperConfig.MapTo<EstoqueDTO>(estoqueDB);
            }
        }

        public IEnumerable<EstoqueDTO> ObterEstoqueNomeProduto(string nome = null)
        {
            using (var context = new RepositoryLojaContext())
            {   
                if (nome != null)
                {
                    var estoqueDB = context.Estoques.Where(e => e.Produto.Nome.Contains(nome));
                    return AutoMapperConfig.MapTo<IEnumerable<EstoqueDTO>>(estoqueDB);
                }
                else
                {
                    var estoqueDB = context.Estoques.ToList();
                    return AutoMapperConfig.MapTo<IEnumerable<EstoqueDTO>>(estoqueDB);
                }                
            }
        }

        public EstoqueDTO IncrementarEstoque(EstoqueDTO estoqueDTO)
        {
            var estoque = ObterEstoqueIdProduto(estoqueDTO.ProdutoId);

            estoque.Quantidade += estoqueDTO.Quantidade;

            AlterarEstoque(estoque);

            return estoque;
        }

        public EstoqueDTO DecrementarEstoque(EstoqueDTO estoqueDTO)
        {
            var estoque = ObterEstoqueIdProduto(estoqueDTO.ProdutoId);

            estoque.Quantidade -= estoqueDTO.Quantidade;

            AlterarEstoque(estoque);

            return estoque;
        }
    }
}

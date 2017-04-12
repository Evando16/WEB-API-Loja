using System;
using System.Collections.Generic;
using Loja.DTO;
using Loja.Repository.Interfaces;
using Loja.MyMappper;
using Loja.ClasseLibrary;
using System.Linq;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace Loja.Repository
{
    public class VendaRepository : IVendaRepository
    {

        public VendaDTO AddVenda(VendaDTO vendaDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                var vendaDB = context.Vendas.Add(AutoMapperConfig.MapTo<Venda>(vendaDTO));
                vendaDB.DataCad = DateTime.Now;
                context.SaveChanges();

                VendaDTO vendaReturn = AutoMapperConfig.MapTo<VendaDTO>(vendaDB);

                foreach (var item in vendaDTO.ListaItens)
                {
                    //adicionar item
                    item.VendaId = vendaReturn.Id;
                    AddItemVenda(item);
                }

                vendaReturn.ListaItens = ObterItensVendaPorIdVenda(vendaReturn.Id);

                return vendaReturn;
            }
        }

        public IEnumerable<ItemVendaDTO> ObterItensVendaPorIdVenda(int idVenda)
        {
            using (var context = new RepositoryLojaContext())
            {
                var listaItensDB = context.ItemVendas.Where(i => i.VendaId == idVenda);
                return AutoMapperConfig.MapTo<IEnumerable<ItemVendaDTO>>(listaItensDB);
            }
        }

        public void AlterarVenda(VendaDTO vendaDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                context.Vendas.AddOrUpdate(AutoMapperConfig.MapTo<Venda>(vendaDTO));
                context.SaveChanges();
            }
        }

        public void DeletarVenda(VendaDTO vendaDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                Venda venda = AutoMapperConfig.MapTo<Venda>(vendaDTO);
                context.Entry(venda).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public VendaDTO ObterVendaPorID(int idVenda)
        {
            using (var context = new RepositoryLojaContext())
            {
                var vendaDTO = AutoMapperConfig.MapTo<VendaDTO>(context.Vendas.SingleOrDefault(v => v.Id == idVenda));

                if (vendaDTO != null)
                {
                    vendaDTO.ListaItens = ObterItensVenda(idVenda);
                }

                return vendaDTO;
            }
        }

        public VendaDTO ObterVendaPorIdProduto(int idProduto)
        {
            using (var context = new RepositoryLojaContext())
            {
                var listaVenda = ObterVendas();

                // Fill the "Vendas" with a list of "ItensVendas"
                foreach (var venda in listaVenda)
                {
                    venda.ListaItens = ObterItensVenda(venda.Id);
                }

                //Check if there is some "Venda" to the product (with idProduto)
                foreach (var venda in listaVenda)
                {
                    foreach (var item in venda.ListaItens)
                    {
                        if (item.ProdutoId == idProduto)
                        {
                            return venda;
                        }
                    }
                }

                return null;
            }
        }

        private IEnumerable<VendaDTO> preencherListavenda(IEnumerable<VendaDTO> listaVendasDB)
        {
            foreach (var venda in listaVendasDB)
            {
                venda.ListaItens = ObterItensVenda(venda.Id);
            }

            return listaVendasDB;
        }

        public IEnumerable<VendaDTO> ObterVendas()
        {
            using (var context = new RepositoryLojaContext())
            {
                var listaVendasDB = AutoMapperConfig.MapTo<IEnumerable<VendaDTO>>(context.Vendas.ToList());

                return preencherListavenda(listaVendasDB);
            }
        }


        public IEnumerable<VendaDTO> ObterVendasDoVendedor(int idVendedor)
        {
            using (var context = new RepositoryLojaContext())
            {
                var listaVendasDB = AutoMapperConfig.MapTo<IEnumerable<VendaDTO>>(context.Vendas.Where(v => v.VendedorId == idVendedor));

                return preencherListavenda(listaVendasDB);
            }
        }

        public IEnumerable<VendaDTO> ObterVendasDoCliente(int idCliente)
        {
            using (var context = new RepositoryLojaContext())
            {
                var listaVendasDB = AutoMapperConfig.MapTo<IEnumerable<VendaDTO>>(context.Vendas.Where(v => v.ClienteId == idCliente));

                return preencherListavenda(listaVendasDB);
            }
        }

        //ITEM VENDA
        public ItemVendaDTO AddItemVenda(ItemVendaDTO item)
        {
            using (var context = new RepositoryLojaContext())
            {
                var itemVendaDB = context.ItemVendas.Add(AutoMapperConfig.MapTo<ItemVenda>(item));
                itemVendaDB.DataCadastro = DateTime.Now;
                context.SaveChanges();

                return AutoMapperConfig.MapTo<ItemVendaDTO>(itemVendaDB);
            }
        }

        public IEnumerable<ItemVendaDTO> ObterItensVenda(int idVenda)
        {
            using (var context = new RepositoryLojaContext())
            {
                var listaItemVendaDB = context.ItemVendas.Where(i => i.VendaId == idVenda);

                return AutoMapperConfig.MapTo<IEnumerable<ItemVendaDTO>>(listaItemVendaDB);
            }
        }

        public void DeletarItemVenda(ItemVendaDTO itemVendaDTO)
        {
            using (var context = new RepositoryLojaContext())
            {
                ItemVenda itemVenda = AutoMapperConfig.MapTo<ItemVenda>(itemVendaDTO);
                context.Entry(itemVenda).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}

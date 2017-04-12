using Business;
using Loja.Business.Interfaces;
using Loja.Repository;
using Loja.Repository.Interfaces;
using SimpleInjector;

namespace Loja.IoC
{
    public class BootStrapper
    {
        public static void Resolve(Container container)
        {
            // For instance:
            //Pessoa
            container.Register<IPessoaBO, PessoaBO>(Lifestyle.Scoped);
            container.Register<IPessoaRepository, PessoaRepository>(Lifestyle.Scoped);

            //Fornecedor
            container.Register<IFornecedorBO, FornecedorBO>(Lifestyle.Scoped);
            container.Register<IFornecedorRepository, FornecedorRepository>(Lifestyle.Scoped);

            //Produto
            container.Register<IProdutoBO, ProdutoBO>(Lifestyle.Scoped);
            container.Register<IProdutosRepository, ProdutoRepository>(Lifestyle.Scoped);

            //Estoque
            container.Register<IEstoqueBO, EstoqueBO>(Lifestyle.Scoped);
            container.Register<IEstoqueRepository, EstoqueRepository>(Lifestyle.Scoped);

            //Venda
            container.Register<IVendaBO, VendaBO>(Lifestyle.Scoped);
            container.Register<IVendaRepository, VendaRepository>(Lifestyle.Scoped);

        }
    }
}

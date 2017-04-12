using AutoMapper;
using Loja.ClasseLibrary;
using Loja.DTO;

namespace Loja.MyMappper
{
    public static class AutoMapperConfig
    {
        public static T MapTo<T>(this object from)
        {
            if (from == null)
                return default(T);

            return Mapper.Map<T>(from);
        }

        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                // other configurations that not use the same properties

                cfg.CreateMap<Cliente, ClienteDTO>().ReverseMap();
                cfg.CreateMap<Vendedor, VendedorDTO>().ReverseMap();

                cfg.CreateMap<PessoaDTO, ClienteDTO>().ReverseMap();
                cfg.CreateMap<PessoaDTO, VendedorDTO>().ReverseMap();
                cfg.CreateMap<Pessoa, PessoaDTO>().ReverseMap();

                cfg.CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();
                cfg.CreateMap<Produto, ProdutoDTO>().ReverseMap();
                cfg.CreateMap<Estoque, EstoqueDTO>().ReverseMap();
                cfg.CreateMap<Venda, VendaDTO>().ReverseMap();
                cfg.CreateMap<ItemVenda, ItemVendaDTO>().ReverseMap();


            });
        }
    }
}

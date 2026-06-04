using AutoMapper;
using ListaDeCompras.WebApplication.ModuloProduto.Aplicacao;

namespace ListaDeCompras.WebApplication.ModuloProduto.Apresentacao;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        // Listar
        CreateMap<ListarProdutosDto, ListarProdutosViewModel>();

        // Cadastrar
        CreateMap<CadastrarProdutoViewModel, CadastrarProdutoDto>();

        // Editar
        CreateMap<EditarProdutoViewModel, EditarProdutoDto>();
        CreateMap<ListarProdutosDto, EditarProdutoViewModel>();

        // Excluir
        CreateMap<ListarProdutosDto, ExcluirProdutoViewModel>();
    }
}

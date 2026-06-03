using AutoMapper;
using ListaDeCompras.WebApplication.ModuloCategoria.Aplicacao;

namespace ListaDeCompras.WebApplication.ModuloCategoria.Apresentacao;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        // Listar
        CreateMap<ListarCategoriasDto, ListarCategoriasViewModel>();

        // Cadastrar
        CreateMap<CadastrarCategoriaViewModel, CadastrarCategoriaDto>();

        // Editar
        CreateMap<EditarCategoriaViewModel, EditarCategoriaDto>();
        CreateMap<DetalhesCategoriaDto, EditarCategoriaViewModel>();

        // Excluir
        CreateMap<DetalhesCategoriaDto, ExcluirCategoriaViewModel>();
    }
}

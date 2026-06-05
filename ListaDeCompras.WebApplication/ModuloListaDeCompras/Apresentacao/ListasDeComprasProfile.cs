using AutoMapper;
using ListaDeCompras.WebApplication.ModuloListaDeCompras.Aplicacao;

namespace ListaDeCompras.WebApplication.ModuloListaDeCompras.Apresentacao;

public class ListasDeComprasProfile : Profile
{
    public ListasDeComprasProfile()
    {
        // Listar
        CreateMap<ListarListasDeComprasDto, ListarListasDeComprasViewModel>();

        // Cadastrar
        CreateMap<CadastrarListasDeComprasViewModel, CadastrarListasDeComprasDto>();

        // Editar
        CreateMap<EditarListasDeComprasViewModel, EditarListasDeComprasDto>();
        CreateMap<ListarListasDeComprasDto, EditarListasDeComprasViewModel>();

        // Excluir
        CreateMap<ListarListasDeComprasDto, ExcluirListasDeComprasViewModel>();
    }
}
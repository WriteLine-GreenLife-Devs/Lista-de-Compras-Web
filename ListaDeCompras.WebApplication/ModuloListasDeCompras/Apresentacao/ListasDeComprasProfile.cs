using AutoMapper;
using ListaDeCompras.WebApplication.ModuloListasDeCompras.Aplicacao;

namespace ListaDeCompras.WebApplication.ModuloListasDeCompras.Apresentacao;

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
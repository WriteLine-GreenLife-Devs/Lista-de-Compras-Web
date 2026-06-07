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
        CreateMap<CadastrarListasDeComprasViewModel, CadastrarListasDeComprasDto>()
            .ConstructUsing(vm => new CadastrarListasDeComprasDto(vm.Nome, DateTime.Now));

        // Editar
        CreateMap<EditarListasDeComprasViewModel, EditarListasDeComprasDto>()
            .ConstructUsing(vm => new EditarListasDeComprasDto(vm.Id, vm.Nome, DateTime.Now));
        CreateMap<ListarListasDeComprasDto, EditarListasDeComprasViewModel>();

        // Excluir
        CreateMap<ListarListasDeComprasDto, ExcluirListasDeComprasViewModel>();
    }
}
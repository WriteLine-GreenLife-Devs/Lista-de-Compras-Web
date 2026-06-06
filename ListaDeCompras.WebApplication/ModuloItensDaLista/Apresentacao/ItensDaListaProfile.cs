using AutoMapper;
using ListaDeCompras.WebApplication.ModuloItensDaLista.Aplicacao;

namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Apresentacao;

public class ItensDaListaProfile : Profile
{
    public ItensDaListaProfile()
    {
        // Listar
        CreateMap<ListarItensDaListaDto, ListarItensDaListaViewModel>();

        // Cadastrar
        CreateMap<CadastrarItensDaListaViewModel, CadastrarItensDaListaDto>();

        // Editar
        CreateMap<EditarItensDaListaViewModel, EditarItensDaListaDto>();
        CreateMap<ListarItensDaListaDto, EditarItensDaListaViewModel>();

        // Excluir
        CreateMap<ListarItensDaListaDto, ExcluirItensDaListaViewModel>();
    }
}
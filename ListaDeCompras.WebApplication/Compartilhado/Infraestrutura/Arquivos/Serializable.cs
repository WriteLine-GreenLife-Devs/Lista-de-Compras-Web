using System.Text.Json;
using System.Text.Json.Serialization;
using ListaDeCompras.WebApplication.ModuloCategoria.Dominio;
using ListaDeCompras.WebApplication.ModuloProduto.Dominio;
using ListaDeCompras.WebApplication.ModuloListaDeCompras.Dominio;
using ListaDeCompras.WebApplication.ModuloItensDaLista.Dominio;

namespace ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;

public sealed class Serializable
{
    #region Adicionar Listas

    public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    public List<Produto> Produtos { get; set; } = new List<Produto>();
    public List<ListasDeCompras> ListasDeCompras { get; set; } = new List<ListasDeCompras>();
    public List<ItensDaLista> ItensDaListas { get; set; } = new List<ItensDaLista>();

    #endregion

    private readonly string caminhoArquivo;

    public Serializable()
    {
        string caminhoAppData = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorio = Path.Combine(caminhoAppData, "ClubeDaLeituraWeb");

        Directory.CreateDirectory(caminhoDiretorio);

        caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");
    }

    public void Salvar()
    {
        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.WriteIndented = true;
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, opcoesJson);

        File.WriteAllText(caminhoArquivo, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return;

        string jsonString = File.ReadAllText(caminhoArquivo);

        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        Serializable? serializableSalvo = JsonSerializer
            .Deserialize<Serializable>(jsonString, opcoesJson);

        if (serializableSalvo == null)
            return;

        #region Atribuir Listas

        Categorias = serializableSalvo.Categorias;
        Produtos = serializableSalvo.Produtos;
        ListasDeCompras = serializableSalvo.ListasDeCompras;
        ItensDaLista = serializableSalvo.ItensDaListas;

        #endregion
    }
}
using System.Text.Json;
using System.Text.Json.Serialization;
using ListaDeCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;

public sealed class Serializable
{
    #region Adicionar Listas

    public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    // public List<Revista> Revistas { get; set; } = new List<Revista>();
    // public List<Amigo> Amigos { get; set; } = new List<Amigo>();
    // public List<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();

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
        // Revistas = serializableSalvo.Revistas;
        // Amigos = serializableSalvo.Amigos;
        // Emprestimos = serializableSalvo.Emprestimos;

        #endregion
    }
}

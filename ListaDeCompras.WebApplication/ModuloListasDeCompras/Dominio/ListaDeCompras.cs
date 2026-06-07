using ListaDeCompras.WebApplication.Compartilhado.Dominio;

namespace ListaDeCompras.WebApplication.ModuloListasDeCompras.Dominio;

public enum StatusLista
{
    Aberta,
    Concluida
}

public sealed class ListasDeCompras : EntidadeBase<ListasDeCompras>
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public StatusLista Status { get; set; } = StatusLista.Aberta;
    public int ItensTotais { get; set; }
    public decimal GastoEstimado { get; set; }

    public ListasDeCompras()
    {
    }

    public ListasDeCompras(string nome, DateTime dataCriacao)
    {
        Nome = nome;
        DataCriacao = dataCriacao;
        Status = StatusLista.Aberta;
        ItensTotais = 0;
        GastoEstimado = 0;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve ter entre 3 e 100 caracteres.");

        return erros;
    }

    public override void Atualizar(ListasDeCompras entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        DataCriacao = entidadeAtualizada.DataCriacao;
        Status = entidadeAtualizada.Status;
        ItensTotais = entidadeAtualizada.ItensTotais;
        GastoEstimado = entidadeAtualizada.GastoEstimado;
    }
}
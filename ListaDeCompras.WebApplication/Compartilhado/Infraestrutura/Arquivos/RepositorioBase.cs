using ListaDeCompras.WebApplication.Compartilhado.Dominio;

namespace ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;

public abstract class RepositorioBase<T> where T : EntidadeBase<T>
{
    protected Serializable serializable;
    protected List<T> registros;

    public RepositorioBase(Serializable serializable)
    {
        this.serializable = serializable;
        this.registros = CarregarRegistros();
    }

    protected abstract List<T> CarregarRegistros();

    public void Cadastrar(T entidade)
    {
        registros.Add(entidade);

        serializable.Salvar();
    }

    public bool Editar(string idSelecionado, T entidadeAtualizada)
    {
        T? registroSelecionado = SelecionarPorId(idSelecionado);

        if (registroSelecionado == null)
            return false;

        registroSelecionado.Atualizar(entidadeAtualizada);

        serializable.Salvar();

        return true;
    }

    public bool Excluir(string idSelecionado)
    {
        T? registroSelecionado = SelecionarPorId(idSelecionado);

        if (registroSelecionado == null)
            return false;

        return Excluir(registroSelecionado);
    }

    public bool Excluir(T registro)
    {
        bool conseguiuExcluir = registros.Remove(registro);

        if (conseguiuExcluir)
            serializable.Salvar();

        return conseguiuExcluir;
    }

    public T? SelecionarPorId(string idSelecionado)
    {
        foreach (T registro in registros)
        {
            if (registro.Id == idSelecionado)
                return registro;
        }

        return null;
    }

    public List<T> SelecionarTodos()
    {
        return registros;
    }

    public List<T> Filtrar(Predicate<T> filtro)
    {
        List<T> registrosFiltrados = new List<T>();

        foreach (T e in registros)
        {
            if (filtro(e))
                registrosFiltrados.Add(e);
        }

        return registrosFiltrados;
    }
}

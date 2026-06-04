using FluentResults;
using ListaDeCompras.WebApplication.Compartilhado.Apresentacao.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ListaDeCompras.WebApplication.Compartilhado.Apresentacao.Extensions;

public static class ModelStateExtensions
{
    public static void AddModelError(this ModelStateDictionary modelState, ResultBase result)
    {
        foreach (IError erro in result.Errors)
        {
            string campo = string.Empty;

            if (erro.Metadata.TryGetValue("Campo", out object? campoMetadata) && campoMetadata is string campoStr)
                campo = campoStr;

            modelState.AddModelError(campo, erro.Message);
        }
    }
}

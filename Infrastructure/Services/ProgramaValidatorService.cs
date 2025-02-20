using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Shared;
using Infrastructure.Exceptions;
using System.Text.Json;

namespace Infrastructure.Services;

public class ProgramaValidatorService : IProgramaValidatorService
{
    public void validate(Programa programa)
    {
        var json = File.ReadAllText(Constants.PROGRAMAS_PADROES_PATH);
        var programasPreDefinidos = JsonSerializer.Deserialize<IEnumerable<Programa>>(json);

        if (programasPreDefinidos != null)
        {
            foreach (var item in programasPreDefinidos)
            {
                if (item.StringAquecimento == programa.StringAquecimento)
                    throw new ApiException("Não é possível adicionar string de aquecimento duplicada.");
            }
        }

        json = File.ReadAllText(Constants.PROGRAMAS_PATH);
        var programasSalvos = JsonSerializer.Deserialize<IEnumerable<Programa>>(json);
        if (programasSalvos != null)
        {
            foreach (var item in programasSalvos)
            {
                if (item.StringAquecimento == programa.StringAquecimento)
                    throw new ApiException("Não é possível adicionar string de aquecimento duplicada.");
            }
        }

    }
}

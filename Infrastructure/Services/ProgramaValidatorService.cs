using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Shared;
using System.Text.Json;

namespace Infrastructure.Services;

class ProgramaValidatorService : IProgramaValidatorService
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
                    return;
                    // TODO: throw exception
            }
        }

        json = File.ReadAllText(Constants.PROGRAMAS_PATH);
        var programasSalvos = JsonSerializer.Deserialize<IEnumerable<Programa>>(json);
        if (programasSalvos == null)
            return;
            // TODO: throw exception

        foreach (var item in programasSalvos)
        {
            if (item.StringAquecimento == programa.StringAquecimento)
                return;
                // TODO: throw exception
        }
    }
}

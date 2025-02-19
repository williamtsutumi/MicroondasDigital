using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;

namespace Infrastructure.Repositories;

public class ProgramaRepository : IProgramaRepository
{
    // Aqui normalmente teria o context, mas optei por fazer tudo em json

    private const string PROGRAMAS_PADROES_PATH = "../Infrastructure/jsons/programas-padroes.json";
    private const string PROGRAMAS_PATH = "../Infrastructure/jsons/programas.json";


    public IEnumerable<Programa> GetProgramasPadroes()
    {
        var json = File.ReadAllText(PROGRAMAS_PADROES_PATH);
        var result = JsonSerializer.Deserialize<IEnumerable<Programa>>(json);

        return (result == null) ? Enumerable.Empty<Programa>() : result;
    }   

    // Implementação ruim de salvamento: necessita ler o .json inteiro para salvar um novo Programa.
    public void CreatePrograma(Programa programa)
    {
        var json = File.ReadAllText(PROGRAMAS_PATH);
        var programasSalvos = JsonSerializer.Deserialize<IEnumerable<Programa>>(json);
        if (programasSalvos == null)
            return;

        programasSalvos = programasSalvos.Append(programa);
        var result = JsonSerializer.Serialize(programasSalvos);
        File.WriteAllText(PROGRAMAS_PATH, result);
    }
}

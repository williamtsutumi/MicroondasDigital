using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Shared;
using System.Text.Json;

namespace Infrastructure.Repositories;

public class ProgramaRepository : IProgramaRepository
{
    // Aqui normalmente teria o context, mas optei por fazer tudo em json
    private readonly IProgramaValidatorService _validator;

    public ProgramaRepository(IProgramaValidatorService validator)
    {
        _validator = validator;
    }

    public IEnumerable<Programa> GetProgramasPadroes()
    {
        var json = File.ReadAllText(Constants.PROGRAMAS_PADROES_PATH);
        var result = JsonSerializer.Deserialize<IEnumerable<Programa>>(json);

        return (result == null) ? Enumerable.Empty<Programa>() : result;
    }

    public IEnumerable<Programa> GetAllCustom()
    {
        var json = File.ReadAllText(Constants.PROGRAMAS_PATH);
        var programasSalvos = JsonSerializer.Deserialize<IEnumerable<Programa>>(json);
        return (programasSalvos == null) ? Enumerable.Empty<Programa>() : programasSalvos;
    }

    public void CreatePrograma(Programa programa)
    {
        _validator.validate(programa);

        var json = File.ReadAllText(Constants.PROGRAMAS_PATH);
        var programasSalvos = JsonSerializer.Deserialize<IEnumerable<Programa>>(json);

        programasSalvos = programasSalvos.Append(programa);
        var result = JsonSerializer.Serialize(programasSalvos);
        File.WriteAllText(Constants.PROGRAMAS_PATH, result);
    }

}

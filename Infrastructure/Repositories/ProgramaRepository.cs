using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;

namespace Infrastructure.Repositories;

public class ProgramaRepository : IProgramaRepository
{
    // Aqui normalmente teria o context, mas optei por fazer tudo em json

    public IEnumerable<Programa> GetProgramasPadroes()
    {
        var json = File.ReadAllText("../Infrastructure/jsons/programas-padroes.json");
        var result = JsonSerializer.Deserialize<IEnumerable<Programa>>(json);

        return (result == null) ? Enumerable.Empty<Programa>() : result;
    }   

    public void CreatePrograma(Programa programa)
    {

    }
}

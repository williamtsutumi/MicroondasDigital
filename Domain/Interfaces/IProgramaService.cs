using Domain.Entities;

namespace Domain.Interfaces;

public interface IProgramaService
{
    IEnumerable<Programa> GetProgramasPadroes();
    void CreatePrograma(Programa programa);
}

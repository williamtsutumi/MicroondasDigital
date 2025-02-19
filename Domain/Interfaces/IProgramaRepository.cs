using Domain.Entities;

namespace Domain.Interfaces;

public interface IProgramaRepository
{
    IEnumerable<Programa> GetProgramasPadroes();
    void CreatePrograma(Programa programa);
}

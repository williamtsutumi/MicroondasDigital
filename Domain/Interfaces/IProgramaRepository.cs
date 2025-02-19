using Domain.Entities;

namespace Domain.Interfaces;

public interface IProgramaRepository
{
    IEnumerable<Programa> GetProgramasPadroes();
    IEnumerable<Programa> GetAllCustom();
    void CreatePrograma(Programa programa);
}

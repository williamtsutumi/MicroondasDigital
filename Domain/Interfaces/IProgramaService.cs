using Domain.Entities;

namespace Domain.Interfaces;

public interface IProgramaService
{
    IEnumerable<Programa> GetProgramasPadroes();
    IEnumerable<Programa> GetAllCustom();
    void CreatePrograma(Programa programa);
}

using Domain.Entities;

namespace Domain.Interfaces;

public interface IMicroondasService
{
    Aquecimento Iniciar(int tempo, int? potencia);
    Aquecimento InicioRapido();
    Aquecimento Acrescento(int tempo, int potencia, string? nomeDoPrograma);
}

using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class MicroondasService : IMicroondasService
{
    public Aquecimento Iniciar(int tempo, int? potencia)
    {
        if (potencia == null)
            potencia = 10;
        else if (potencia < 0 || potencia > 10)
        {
            //return BadRequest("Valor inválido para potência!");
        }

        if (tempo < 0 || tempo > 200)
        {
            //return BadRequest("Valor inválido para o tempo!");
        }
        return new Aquecimento(GetTimeSpanFromTempo(tempo), potencia.Value);
    }

    public Aquecimento InicioRapido()
    {
        return new Aquecimento(new TimeSpan(0, 0, 30), 10);
    }

    public Aquecimento Acrescento(int tempo, int potencia, string? nomeDoPrograma)
    {
        if (nomeDoPrograma != null)
            return new Aquecimento();
            //throw new Exception("Não é possível acrescentar tempo para programas pre-definidos");

        var timeSpan = GetTimeSpanFromTempo(tempo);
        timeSpan += TimeSpan.FromSeconds(30);

        return new Aquecimento(timeSpan, potencia);
    }

    private TimeSpan GetTimeSpanFromTempo(int tempo)
    {
        var tempoSegundos = 0;
        var tempoMinutos = 0;

        if (tempo > 60 && tempo < 100)
        {
            tempoSegundos = tempo % 60;
            tempoMinutos = 1;
        }
        else
        {
            tempoSegundos = tempo % 100;
            tempoMinutos = tempo / 100;
        }
        return new TimeSpan(0, tempoMinutos, tempoSegundos);
    }
}

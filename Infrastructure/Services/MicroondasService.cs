using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Exceptions;

namespace Infrastructure.Services;

public class MicroondasService : IMicroondasService
{
    public Aquecimento Iniciar(int tempo, int? potencia, string? nomeDoPrograma)
    {
        if (potencia == null)
            potencia = 10;

        if (nomeDoPrograma == null)
        {
            if (potencia < 1 || potencia > 10)
                throw new ApiException("Valor de potência inválido. Favor utilizar um valor de 1 a 10");

            if (tempo < 1 || tempo > 200)
                throw new ApiException("Valor de tempo inválido. Favor utilizar um valor de 1 a 200.");
        }

        return new Aquecimento(GetTimeSpanFromTempo(tempo), potencia.Value);
    }

    public Aquecimento InicioRapido()
    {
        return new Aquecimento(new TimeSpan(0, 0, 30), 10);
    }

    public Aquecimento Acrescento(int tempo, int? potencia, string? nomeDoPrograma)
    {
        if (potencia == null)
            throw new ApiException("O valor de potência deve ser informado.");

        if (nomeDoPrograma != null)
            throw new ApiException("Não é possível incrementar o tempo de um programa pré-definido.");

        var timeSpan = GetTimeSpanFromTempo(tempo);
        timeSpan += TimeSpan.FromSeconds(30);

        return new Aquecimento(timeSpan, potencia.Value);
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

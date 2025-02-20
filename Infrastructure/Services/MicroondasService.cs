using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class MicroondasService : IMicroondasService
{
    private readonly IMicroondasValidatorService _validator;

    public MicroondasService(IMicroondasValidatorService validator)
    {
        _validator = validator;
    }

    public Aquecimento Iniciar(int tempo, int? potencia, string? nomeDoPrograma)
    {
        _validator.validateIniciar(tempo, potencia, nomeDoPrograma);

        if (potencia == null)
            potencia = 10;

        return new Aquecimento(GetTimeSpanFromTempo(tempo), potencia.Value);
    }

    public Aquecimento InicioRapido()
    {
        return new Aquecimento(new TimeSpan(0, 0, 30), 10);
    }

    public Aquecimento Acrescento(int tempo, int? potencia, string? nomeDoPrograma)
    {
        _validator.validateAcrescento(potencia, nomeDoPrograma);

        var timeSpan = GetTimeSpanFromTempo(tempo);
        timeSpan += TimeSpan.FromSeconds(30);

        return new Aquecimento(timeSpan, potencia!.Value);
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

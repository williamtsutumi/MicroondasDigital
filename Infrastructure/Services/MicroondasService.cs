using Domain.Interfaces;

namespace Infrastructure.Services;

public class MicroondasService : IMicroondasService
{
    public Tuple<int, int> Iniciar(int? tempo, int? potencia)
    {
        if (potencia == null)
            potencia = 5;
        else if (potencia < 0 || potencia > 10)
            return new Tuple<int,int>(0,0);
            //return BadRequest("Valor inválido para potência!");

        var tempoSegundos = 0;
        var tempoMinutos = 0;

        if (tempo == null)
        {
            tempoSegundos = 30;
            tempoMinutos = 0;
        }
        else if (tempo < 0 || tempo > 200)
            return new Tuple<int, int>(0, 0);
            //return BadRequest("Valor inválido para o tempo!");
        else if (tempo > 60 && tempo < 100)
        {
            tempoSegundos = tempo.Value % 60;
            tempoMinutos = 1;
        }
        else
        {
            tempoSegundos = tempo.Value % 100;
            tempoMinutos = tempo.Value / 100;
        }
        return new Tuple<int, int>(tempoMinutos * 60 + tempoSegundos, potencia.Value);
    }
}

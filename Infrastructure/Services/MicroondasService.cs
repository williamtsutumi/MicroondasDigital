using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class MicroondasService : IMicroondasService
{
    public Aquecimento Iniciar(int tempo, int potencia)
    {    
        if (potencia < 0 || potencia > 10)
        {
            //return BadRequest("Valor inválido para potência!");
        }

        var tempoSegundos = 0;
        var tempoMinutos = 0;

        if (tempo < 0 || tempo > 200)
        {
            //return BadRequest("Valor inválido para o tempo!");
        }
        else if (tempo > 60 && tempo < 100)
        {
            tempoSegundos = tempo % 60;
            tempoMinutos = 1;
        }
        else
        {
            tempoSegundos = tempo % 100;
            tempoMinutos = tempo / 100;
        }

        var tempoSpan = new TimeSpan(0, tempoMinutos, tempoSegundos);
        return new Aquecimento(tempoSpan, potencia);
    }

    public Aquecimento InicioRapido()
    {
        return new Aquecimento(new TimeSpan(0, 0, 30), 10);
    }

    public Aquecimento Acrescento(int tempo, int potencia)
    {
        return new Aquecimento(new TimeSpan(0, 0, 30), potencia);
        //return new Aquecimento(tempo + new TimeSpan(0, 0, 30), potencia);
    }

    public Aquecimento Pausa(int tempo, int potencia, bool jaEstaPausado)
    {
        return new Aquecimento(new TimeSpan(0, 0, 0), 0);
        //if (jaEstaPausado)
        //    return new Aquecimento(new TimeSpan(0, 0, 0), 0);
        //else
        //return new Aquecimento();
    }
}

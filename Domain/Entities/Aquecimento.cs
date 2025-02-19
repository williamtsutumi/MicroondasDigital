namespace Domain.Entities;

public class Aquecimento
{
    public TimeSpan Tempo { get; set; }
    public int Potencia { get; set; }

    public Aquecimento(TimeSpan tempo, int potencia)
    {
        Tempo = tempo;
        Potencia = potencia;
    }

    public Aquecimento()
    {
        Tempo = new TimeSpan(0, 0, 0);
        Potencia = 0;
    }
}

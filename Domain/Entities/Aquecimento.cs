namespace Domain.Entities;

public class Aquecimento
{
    public TimeSpan Tempo { get; set; }
    public int Potencia { get; set; }
    public DateTime Inicio { get; set; }

    public Aquecimento(TimeSpan tempo, int potencia)
    {
        Tempo = tempo;
        Potencia = potencia;
    }
}

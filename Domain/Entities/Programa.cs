namespace Domain.Entities;

public class Programa
{
    public String Nome { get; set; }
    public String Alimento { get; set; }
    public TimeSpan Tempo { get; set; }
    public int Potencia { get; set; }
    public String StringAquecimento { get; set; }
    public String? Instrucoes { get; set; }
}

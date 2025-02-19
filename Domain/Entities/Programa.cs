namespace Domain.Entities;

public class Programa
{
    private Guid Id { get; set; }
    private String Alimento { get; set; }
    private TimeSpan Tempo { get; set; }
    private String Potencia { get; set; }
    private String? Instrucoes { get; set; }
}

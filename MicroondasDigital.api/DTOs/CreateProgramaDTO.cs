namespace Presentation.DTOs;

public record CreateProgramaDTO
{
    public string Nome { get; set; }
    public string Alimento { get; set; }
    public TimeSpan Tempo { get; set; }
    public int Potencia { get; set; }
    public string Instrucoes { get; set; }

}

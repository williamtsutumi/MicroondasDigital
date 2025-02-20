namespace Presentation.DTOs;

public record ProgramaDTO
{
    public string Nome { get; set; }
    public string Alimento { get; set; }
    public TimeSpan Tempo { get; set; }
    public int Potencia { get; set; }
    public string StringAquecimento { get; set; }
    public string? Instrucoes { get; set; }

}

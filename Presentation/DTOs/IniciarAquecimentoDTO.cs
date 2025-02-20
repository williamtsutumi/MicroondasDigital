namespace Presentation.DTOs;

public record IniciarAquecimentoDTO
{
    public int Tempo { get; set; }
    public int? Potencia { get; set; }
    public string? NomeDoPrograma { get; set; }
}

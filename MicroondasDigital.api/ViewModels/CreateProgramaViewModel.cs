namespace MicroondasDigital.api.ViewModels;

public record CreateProgramaViewModel
{
    public string Nome { get; set; }
    public string Alimento { get; set; }
    public TimeSpan Tempo { get; set; }
    public int Potencia { get; set; }
    public string Instrucoes { get; set; }

}

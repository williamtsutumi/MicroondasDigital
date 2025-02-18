namespace MicroondasDigital.api.ViewModels
{
    public record IniciarAquecimentoViewModel
    {
        public int? Tempo { get; set; }
        public int? Potencia { get; set; }
    }
}

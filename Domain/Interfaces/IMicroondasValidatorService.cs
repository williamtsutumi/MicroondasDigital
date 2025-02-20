namespace Domain.Interfaces;

public interface IMicroondasValidatorService
{
    void validateIniciar(int tempo, int? potencia, string? nomeDoPrograma);
    void validateAcrescento(int? potencia, string? nomeDoPrograma);
}

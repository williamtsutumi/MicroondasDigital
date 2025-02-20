using Domain.Interfaces;
using Infrastructure.Exceptions;

namespace Infrastructure.Services;

public class MicroondasValidatorService : IMicroondasValidatorService
{
    public void validateIniciar(int tempo, int? potencia, string? nomeDoPrograma)
    {
        if (nomeDoPrograma == null)
        {
            if (potencia != null && (potencia < 1 || potencia > 10))
                throw new ApiException("Valor de potência inválido. Favor utilizar um valor de 1 a 10.");

            if (tempo < 1 || tempo > 200)
                throw new ApiException("Valor de tempo inválido. Favor utilizar um valor de 1 a 200.");
        }
    }
    public void validateAcrescento(int? potencia, string? nomeDoPrograma)
    {
        if (potencia == null)
            throw new ApiException("O valor de potência deve ser informado.");

        if (nomeDoPrograma != null)
            throw new ApiException("Não é possível incrementar o tempo de um programa pré-definido.");
    }
}

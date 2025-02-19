namespace Domain.Interfaces;

public interface IMicroondasService
{
    Tuple<int,int> Iniciar(int? tempo, int? potencia);
}

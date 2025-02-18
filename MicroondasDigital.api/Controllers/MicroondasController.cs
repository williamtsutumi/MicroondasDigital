using MicroondasDigital.api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MicroondasDigital.api.Controllers;

[ApiController]
[Route("[controller]")]
public class MicroondasController : ControllerBase
{

    private readonly ILogger<MicroondasController> _logger;

    public MicroondasController(ILogger<MicroondasController> logger)
    {
        _logger = logger;
    }

    [HttpPost("iniciar")]
    public IActionResult Iniciar([FromBody] IniciarAquecimentoViewModel viewModel)
    {
        if (viewModel.Potencia < 0 || viewModel.Potencia > 10)
            return BadRequest("Valor inválido para potência!");
        if (viewModel.Tempo < 0 || viewModel.Tempo > 200)
            return BadRequest("Valor inválido para o tempo!");

        var tempoSegundos = 0;
        var tempoMinutos = 0;
        var potencia = 0;
        if (viewModel.Potencia == null)
        {
            potencia = 5;
        }
        if (viewModel.Tempo == null)
        {
            tempoSegundos = 30;
            tempoMinutos = 0;
        }
        else if (viewModel.Tempo.Value > 60 && viewModel.Tempo.Value < 100)
        {
            tempoSegundos = viewModel.Tempo.Value % 60;
            tempoMinutos = 1;
        }
        else
        {
            tempoSegundos = viewModel.Tempo.Value % 100;
            tempoMinutos = viewModel.Tempo.Value / 100;
        }

        return Ok(new
        {
            Tempo = tempoSegundos,
            Potencia = potencia
        });
    }
}

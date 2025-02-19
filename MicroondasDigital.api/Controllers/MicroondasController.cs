using Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class MicroondasController : ControllerBase
{

    private readonly IMicroondasService _service;

    public MicroondasController(IMicroondasService service)
    {
        _service = service;
    }

    [HttpPost("iniciar")]
    public IActionResult Iniciar([FromBody] IniciarAquecimentoDTO viewModel)
    {
        var result = _service.Iniciar(viewModel.Tempo, viewModel.Potencia);
        return Ok(new
        {
            Tempo = result.Item1,
            Potencia = result.Item2
        });
    }
}

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("MyPolicy")] // Configuração CORS local no controller. Idealmente seria global, mas estou com pouco tempo!
public class MicroondasController : ControllerBase
{

    private readonly IMicroondasService _service;

    public MicroondasController(IMicroondasService service)
    {
        _service = service;
    }

    [HttpPost("iniciar")]
    public IActionResult Iniciar([FromBody] IniciarAquecimentoDTO request)
    {
        Aquecimento result = _service.Iniciar(request.Tempo, request.Potencia, request.NomeDoPrograma);
        return Ok(new
        {
            result.Tempo,
            result.Potencia
        });
    }

    [HttpPost("inicio-rapido")]
    public IActionResult InicioRapido()
    {
        Aquecimento result = _service.InicioRapido();
        return Ok(new
        {
            result.Tempo,
            result.Potencia
        });
    }

    [HttpPost("acrescento")]
    public IActionResult Acrescento([FromBody] IniciarAquecimentoDTO request)
    {
        Aquecimento result = _service.Acrescento(request.Tempo, request.Potencia, request.NomeDoPrograma);
        return Ok(new
        {
            result.Tempo,
            result.Potencia
        });
    }
}

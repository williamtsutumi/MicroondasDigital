using Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Entities;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ProgramaController : ControllerBase
{
    private readonly IProgramaService _service;

    public ProgramaController(IProgramaService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult CreatePrograma([FromBody] ProgramaDTO request)
    {
        _service.CreatePrograma(new Programa()
        {
            Nome = request.Nome,
            Alimento = request.Alimento,
            Tempo = request.Tempo,
            Potencia = request.Potencia,
            StringAquecimento = request.StringAquecimento,
            Instrucoes = request.Instrucoes
        });
        return Ok();
    }

    [HttpGet("programas-customizados")]
    public IActionResult GetAllCustom()
    {
        var result = _service.GetAllCustom();
        return Ok(new
        {
            Programas = result
        });
    }

    [HttpGet("programas-padroes")]
    public IActionResult GetProgramasPadroes()
    {
        var result = _service.GetProgramasPadroes();
        return Ok(result);
    }
}

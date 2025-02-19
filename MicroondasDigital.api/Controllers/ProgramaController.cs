using Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;

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
    public IActionResult CreatePrograma([FromBody] ProgramaDTO viewModel)
    {
        return Ok(new
        {
        });
    }

    [HttpGet("get-programas-padroes")]
    public IActionResult GetProgramasPadroes()
    {
        var result = _service.GetProgramasPadroes();
        return Ok(result);
    }
}

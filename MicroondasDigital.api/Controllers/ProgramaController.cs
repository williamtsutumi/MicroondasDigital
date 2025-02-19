using Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ProgramaController : ControllerBase
{
    private readonly ILogger<ProgramaController> _logger;

    public ProgramaController(ILogger<ProgramaController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreatePrograma([FromBody] CreateProgramaDTO viewModel)
    {
        return Ok(new
        {
        });
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        return Ok(new
        {
        });
    }
}

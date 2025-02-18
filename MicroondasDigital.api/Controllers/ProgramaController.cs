using MicroondasDigital.api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MicroondasDigital.api.Controllers;

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
    public IActionResult CreatePrograma([FromBody] CreateProgramaViewModel viewModel)
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

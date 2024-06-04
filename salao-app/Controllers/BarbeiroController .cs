using Microsoft.AspNetCore.Mvc;
using salao_app.Models.Requests;
using salao_app.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class BarbeiroController : ControllerBase
{
    private readonly IBarbeiroService _barbeiroService;

    public BarbeiroController(IBarbeiroService barbeiroService)
    {
        _barbeiroService = barbeiroService;
    }

    [HttpPost("AdicionarHorario")]
    public IActionResult AdicionarHorarioDisponivel([FromBody] AdicionarHorarioDisponivelRequest request)
    {
        try
        {
            _barbeiroService.AdicionarHorarioDisponivel(request.BarbeiroId, request.Data, request.HoraInicio, request.HoraFim);
            return Ok(new { message = "Horário adicionado com sucesso." });  // Alteração aqui
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });  // Alteração aqui
        }
    }


    [HttpGet("ListarHorariosDisponiveis")]
    public IActionResult ListarHorariosDisponiveis(int barbeiroId)
    {
        try
        {
            var horarios = _barbeiroService.ListarHorariosDisponiveis(barbeiroId);
            return Ok(horarios);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ListarHorariosDisponiveisPorData")]
    public IActionResult ListarHorariosDisponiveisPorData(int barbeiroId, DateTime data)
    {
        var horarios = _barbeiroService.ListarHorariosDisponiveisPorData(barbeiroId, data);
        return Ok(horarios);
    }

}


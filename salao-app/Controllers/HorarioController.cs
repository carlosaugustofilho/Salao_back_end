using Microsoft.AspNetCore.Mvc;
using salao_app.Models;
using salao_app.Business.Interfaces;
using SalaoApp.Models;
using Microsoft.Extensions.Logging;

namespace salao_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioService _horarioService;
        private readonly ILogger<HorarioController> _logger;
        public HorarioController(IHorarioService horarioService, ILogger<HorarioController> logger)
        {
            _horarioService = horarioService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/api/Horario/BuscarTodosHorarios/")]
        public IActionResult BuscarTodosHorarios(DateTime data)
        {
            var horarios = _horarioService.BuscarTodosHorarios(data);
            return Ok(horarios);
        }

        [HttpGet]
        [Route("/api/Horario/BuscarHorarioPorId/")]
        public IActionResult BuscarHorarioPorId(int id)
        {
            var horario = _horarioService.BuscarHorarioPorId(id);
            if (horario == null)
            {
                return NotFound();
            }
            return Ok(horario);
        }

        //[HttpPost]
        //[Route("/api/Horario/AgendarHorario")]
        //public IActionResult AgendarHorario([FromBody] HorariolMap horario)
        //{
        //    try
        //    {
        //        _horarioService.AgendarHorario(horario);
        //        return CreatedAtAction(nameof(AgendarHorario), new { id = horario.HorarioId }, horario);
        //    }
        //        catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao agendar horário: {Message}", ex.Message);
        //        return BadRequest($"Houve um erro: {ex.Message}");
        //    }
        //}

        //[HttpPost]
        //[Route("/api/Horario/AgendarHorario")]
        //public IActionResult AgendarHorario([FromBody] HorariolMap horario)
        //{
        //    _horarioService.AgendarHorario(horario);
        //    return CreatedAtAction(nameof(AgendarHorario), new { id = horario.HorarioId }, horario);
        //}

        [HttpDelete]
        [Route("/api/HorarioDisponivel/DeletarHorario/")]
        public IActionResult DeleteHorario(int id)
        {
            var existingHorario = _horarioService.BuscarHorarioPorId(id);
            if (existingHorario == null)
            {
                return NotFound();
            }
            _horarioService.DeletarHorario(id);
            return NoContent();
        }
    }
}

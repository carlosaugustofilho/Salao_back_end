using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using salao_app.Business.Interfaces;
using salao_app.Business.Services;
using salao_app.Models.DTOs;
using salao_app.Models.Requests;

namespace salao_app.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClientesService _service;
        private readonly ILogger<HorarioController> _logger;


        public ClientesController(IClientesService service, ILogger<HorarioController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("/api/Cliente/BuscarClientes/{id}")]
        public ActionResult<ClienteDto> BuscarClientesId([FromRoute] int id)
        {
            try
            {
                ClienteDto cliente = _service.BuscarClientesId(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return Ok(cliente);
            }
            catch (Exception)
            {
                return BadRequest("Houve um erro, por favor tente novamente mais tarde!");
            }
        }

        [HttpGet]
        [Route("/api/Cliente/BuscarClientes")]
        public ActionResult<List<ClienteDto>> BuscarClientes([FromQuery] string nome = "", string email = "")
        {
            try
            {
                var clientes = _service.BuscarClientes(nome, email);
                return Ok(clientes);
            }
            catch (Exception)
            {
                return BadRequest("Houve um erro, por favor tente novamente mais tarde!");
            }
        }

        [HttpPost]
        [Route("/api/Cliente/CriarCliente")]
        public ActionResult CriarCliente([FromBody] ClienteRequest request)
        {
            try
            {
                _service.CriarCliente(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um erro: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("/api/Cliente/AgendarHorarioCliente")]
        public IActionResult AgendarHorarioCliente([FromBody] AgendamentoRequest request)
        {
            try
            {
                if (request == null || request.ClienteId <= 0 || request.BarbeiroId <= 0 || request.Data == default(DateTime) || request.HoraInicio == default(TimeSpan) || request.HoraFim == default(TimeSpan))
                {
                    _logger.LogWarning("Dados inválidos para agendamento. Request: {@Request}", request);
                    return BadRequest("Dados inválidos para agendamento.");
                }

                _service.AgendarHorarioCliente(request.ClienteId, request.BarbeiroId, request.Data, request.HoraInicio, request.HoraFim, request.UsuarioId);
                return Ok(new { message = "Agendamento realizado com sucesso." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao agendar horário para ClienteID: {ClienteId}, BarbeiroID: {BarbeiroId}", request?.ClienteId, request?.BarbeiroId);
                return BadRequest($"Houve um erro: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("CancelarAgendamento")]
        public IActionResult CancelarAgendamento([FromBody] CancelarAgendamentoRequest request)
        {
            try
            {
                if (request == null || request.AgendamentoId <= 0)
                {
                    return BadRequest("O campo agendamentoId é obrigatório.");
                }

                _service.CancelarAgendamento(request.AgendamentoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um erro: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AtualizarStatus")]
        public IActionResult AtualizarStatus(int id, int disponivel)
        {
            try
            {
                bool statusDisponivel = disponivel == 1;
                _service.AtualizarStatusHorario(id, statusDisponivel);
                return Ok(new { message = "Status do horário atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do horário: {Message}", ex.Message);
                return BadRequest($"Houve um erro: {ex.Message}");
            }
        }



        [HttpPost]
        [Route("/api/Cliente/AtualizaDadosCliente")]
        public ActionResult AtualizaDadosCliente([FromBody] ClienteRequest request)
        {
            try
            {
                _service.AtualizaDadosCliente(request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Houve um erro, por favor tente novamente mais tarde!");
            }
        }

        [HttpPost]
        [Route("/api/Cliente/AtivaCliente/{id}")]
        public ActionResult AtivaCliente([FromRoute] int id)
        {
            try
            {
                _service.AlterarStatusCliente(id, false);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Houve um erro, por favor tente novamente mais tarde!");
            }
        }

        [HttpGet("Barbeiro/{barbeiroId}/HorariosDisponiveis")]
        public IActionResult ObterHorariosDisponiveis(int barbeiroId)
        {
            try
            {
                var horarios = _service.ObterHorariosDisponiveis(barbeiroId);
                if (horarios == null || !horarios.Any())
                {
                    return NotFound("Nenhum horário disponível encontrado.");
                }
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um erro: {ex.Message}");
            }
        }
    }
}

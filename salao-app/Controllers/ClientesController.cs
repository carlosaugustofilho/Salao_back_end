using Microsoft.AspNetCore.Mvc;
using salao_app.Business.Interfaces;
using salao_app.Models.DTOs;
using salao_app.Models.Requests;

namespace salao_app.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClientesService _service;
     
        public ClientesController(IClientesService service)
        {
            _service = service;
           
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
                var clientes = _service.BuscarClientes( nome, email);
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
                if (request == null || request.HoraInicio == null || request.HoraInicio == null)
                {
                    return BadRequest("Os campos clienteId, barbeiroId, data, horaInicio e horaFim são obrigatórios.");
                }

                _service.AgendarHorarioCliente(request.ClienteId, request.BarbeiroId, request.Data, request.HoraInicio, request.HoraFim, request.UsuarioId);

                return Ok();
            }
            catch (Exception ex)
            {
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
    }
}

//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using salao_app.Business.Interfaces;
//using salao_app.Models.Requests;
//using salao_app.Models.Dto;
//using System.Threading.Tasks;

//[ApiController]
//[Route("api/[controller]")]
//public class UsuarioController : ControllerBase
//{
//    private readonly IUsuarioService _usuarioService;

//    public UsuarioController(IUsuarioService usuarioService)
//    {
//        _usuarioService = usuarioService;
//    }

//    [AllowAnonymous]
//    [HttpPost("Login")]
//    public IActionResult Login([FromBody] LoginRequest request)
//    {
//        var user = _usuarioService.Login(request.Cpf, request.Senha);
//        if (user == null)
//            return Unauthorized(new { message = "Usuário ou senha inválidos" });

//        var token = _usuarioService.GenerateJwtToken(user);
//        return Ok(new { token });
//    }

//    [HttpGet("{id}")]
//    [Authorize]
//    public IActionResult GetUserById(int id)
//    {
//        var user = _usuarioService.BuscarUsuarioId(id);
//        if (user == null)
//            return NotFound();

//        return Ok(user);
//    }

//    [HttpPost]
//    [Authorize(Roles = "Admin")]
//    public IActionResult CreateUser([FromBody] NovoUsuarioRequest request)
//    {
//        var user = _usuarioService.CadastroUsuario(request);
//        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
//    }
//}

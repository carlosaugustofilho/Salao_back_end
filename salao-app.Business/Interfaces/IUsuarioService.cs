using salao_app.Models.Dto;
using salao_app.Models.Requests;
using salao_app.Repository.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salao_app.Business.Interfaces
{
    public interface IUsuarioService 
    {
        void BuscarNovoUsuarios(NovoUsuarioRequest request);
        object BuscarTiposUsuarios();
        object BuscarUsuarioId(int id);
        object CadastroUsuario(UsuarioMap usuario);
        void DeleteUsuario(int id);
        object GenerateJwtToken(UsuarioDto user);
        object ListaUsuarios();
        UsuarioDto Login(string cpf, string senha);
        void UpdateUsuario(UsuarioMap usuario);
    }
}

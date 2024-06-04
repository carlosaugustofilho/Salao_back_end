using salao_app.Repository.Maps;
using System.Collections.Generic;

namespace salao_app.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        List<UsuarioMap> ListaUsuarios();
        UsuarioMap BuscarUsuarioId(int id);
        void CadastroUsuario(UsuarioMap usuario);
        void UpdateUsuario(UsuarioMap usuario);
        void DeleteUsuario(int id);
        UsuarioMap Login(string cpf, string senha);
        object BuscarTiposUsuarios();
    }
}

using salao_app.Repository.Intefaces;
using salao_app.Repository.Interfaces;
using salao_app.Repository.Maps;

namespace salao_app.Repository.Services
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public List<UsuarioMap> CadastroUsuario(UsuarioMap usuario)
        {
            throw new NotImplementedException();
        }

        public UsuarioMap BuscarUsuarioId(int id)
        {
            throw new NotImplementedException();
        }

        public List<UsuarioMap> ListaUsuarios()
        {
            throw new NotImplementedException();
        }

        public void UpdateUsuario(UsuarioMap usuario)
        {
            throw new NotImplementedException();
        }

       

        public void DeleteUsuario(int id)
        {
            throw new NotImplementedException();
        }

        void IUsuarioRepository.CadastroUsuario(UsuarioMap usuario)
        {
            throw new NotImplementedException();
        }

        public UsuarioMap Login(string cpf, string senha)
        {
            throw new NotImplementedException();
        }

        public object BuscarTiposUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}

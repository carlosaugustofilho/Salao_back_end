using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salao_app.Repository.Maps
{
    public class UsuarioMap
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int PapelId { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}

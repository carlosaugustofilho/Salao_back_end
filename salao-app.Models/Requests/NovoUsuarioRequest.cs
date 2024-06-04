using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salao_app.Models.Requests
{
    public class NovoUsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }
    }
}

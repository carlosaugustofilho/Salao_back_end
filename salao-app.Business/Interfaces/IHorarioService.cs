using salao_app.Models;
using SalaoApp.Models;

namespace salao_app.Business.Interfaces
{
    public interface IHorarioService
    {
        List<HorariolMap> BuscarTodosHorarios(DateTime data);
        HorariolMap BuscarHorarioPorId(int horarioId);
       // void AgendarHorario(HorariolMap horario);
        void AtualizarHorario(HorariolMap horario);
        void DeletarHorario(int horarioId);
    }
}

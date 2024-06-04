using System;

namespace salao_app.Repository.Services
{
    public interface IBarbeiroRepository
    {
        void AdicionarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim);
        List<HorarioDisponivelMap> ListarHorariosDisponiveis(int barbeiroId);
        List<HorarioDisponivelMap> ListarHorariosDisponiveisPorData(int barbeiroId, DateTime data);
    }
}

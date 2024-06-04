namespace salao_app.Services.Interfaces
{
    public interface IBarbeiroService
    {
        void AdicionarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim);

        List<HorarioDisponivelMap> ListarHorariosDisponiveis(int barbeiroId);

        List<HorarioDisponivelMap> ListarHorariosDisponiveisPorData(int barbeiroId, DateTime data);

    }
}

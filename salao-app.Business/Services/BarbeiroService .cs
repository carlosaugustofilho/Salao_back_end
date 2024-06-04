using salao_app.Repository.Interfaces;
using salao_app.Repository.Services;
using salao_app.Services.Interfaces;
using System;

namespace salao_app.Services
{
    public class BarbeiroService : IBarbeiroService
    {
        private readonly IBarbeiroRepository _barbeiroRepository;

        public BarbeiroService(IBarbeiroRepository barbeiroRepository)
        {
            _barbeiroRepository = barbeiroRepository;
        }

        public void AdicionarHorarioDisponivel(int barbeiroId, DateTime data,  TimeSpan horaInicio, TimeSpan horaFim)
        {
            var duracaoAtendimento = TimeSpan.FromMinutes(45);
            var horaAtual = horaInicio;

            while (horaAtual + duracaoAtendimento <= horaFim)
            {
                _barbeiroRepository.AdicionarHorarioDisponivel(barbeiroId, data,  horaAtual, horaAtual + duracaoAtendimento);
                horaAtual += duracaoAtendimento;
            }
        }

        public List<HorarioDisponivelMap> ListarHorariosDisponiveis(int barbeiroId)
        {
            return _barbeiroRepository.ListarHorariosDisponiveis(barbeiroId);
        }

        public List<HorarioDisponivelMap> ListarHorariosDisponiveisPorData(int barbeiroId, DateTime data)
        {
            return _barbeiroRepository.ListarHorariosDisponiveisPorData(barbeiroId, data);
        }

    }


}


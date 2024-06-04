using carvao_app.Repository.Conexao;
using Dapper;
using Microsoft.Extensions.Configuration;
using salao_app.Repository.Interfaces;
using SalaoApp.Models;

namespace salao_app.Repository.Services
{
    public class HorarioRepository : IHorarioRepository
    {
        private readonly IConfiguration _configuration;

        public HorarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HorariolMap BuscarHorarioPorId(int horarioId)
        {
            var horario = DataBase.Execute<HorariolMap>(_configuration, "SELECT * FROM horario WHERE horario_id = @Id", new { Id = horarioId }).FirstOrDefault();
            return horario;
        }
        public List<HorariolMap> BuscarTodosHorarios(DateTime data)
        {
            throw new NotImplementedException();
        }


        //public List<HorariolMap> BuscarTodosHorarios(DateTime data, int BarbeiroId)
        //{
        //    if (data == null)
        //        throw new ArgumentException("Data inválida.");

        //    var parameters = new DynamicParameters();
        //    parameters.Add("@data", data);
        //    parameters.Add("@barbeiro_id", BarbeiroId);

        //    var query = @"
        //        SELECT mh.horario_id AS horarioId, mh.barbeiro_id AS barbeiroId, mh.data, 
        //       mh.hora_inicio AS horaInicio, mh.hora_fim AS horaFim,
        //       COALESCE(a.agendamento_id, 0) AS agendamento_id, 
        //       b.nome AS nome_barbeiro, c.nome AS nome_cliente
        //        FROM marcacao_horarios mh
        //        LEFT JOIN agendamentos a ON mh.horario_id = a.horario_disponiveis_id AND DATE(a.data_hora_agendamento) = DATE(@data)
        //        LEFT JOIN barbeiros b ON mh.barbeiro_id = b.barbeiro_id
        //        LEFT JOIN cliente c ON a.usuario_id = c.usuario_id
        //        WHERE mh.hora_inicio >= @barbeiro_id";

        //    using var connection = DataBase.Dbo(_configuration);
        //    var horarios = connection.Query<HorariolMap>(query, parameters).ToList();

        //    return horarios;
        //}

        //public void AgendarHorario(HorariolMap horario)
        //{
        //    if (horario.Data == DateTime.MinValue)
        //        horario.Data = DateTime.Now.Date;

        //    if (horario.Data < DateTime.Now.Date)
        //        throw new ArgumentException("Não é possível agendar horários para datas anteriores ao dia atual.");

        //    if (horario.HoraInicio == TimeSpan.Zero || horario.HoraFim == TimeSpan.Zero)
        //        throw new ArgumentException("Horas de início e fim não podem ser nulas.");

        //    if (horario.BarbeiroId <= 0)
        //        throw new ArgumentException("Identificador do barbeiro é inválido.");

        //    if (!VerificarHorarioDisponivel(horario.BarbeiroId, horario.Data, horario.HoraInicio, horario.HoraFim))
        //        throw new Exception("Não existe horário disponível para este dia e hora.");

        //    var parametersMarcacao = new DynamicParameters();
        //    parametersMarcacao.Add("@barbeiro_id", horario.BarbeiroId);
        //    parametersMarcacao.Add("@cliente_id", horario.Cliente.clienteId);
        //    parametersMarcacao.Add("@data", horario.Data.Date);
        //    parametersMarcacao.Add("@hora_inicio", horario.HoraInicio);
        //    parametersMarcacao.Add("@hora_fim", horario.HoraFim);

        //    var queryMarcacao = @"
        //        INSERT INTO marcacao_horarios (barbeiro_id, cliente_id, data, hora_inicio, hora_fim) 
        //        VALUES (@barbeiro_id, @cliente_id, @data, @hora_inicio, @hora_fim);
        //        SELECT LAST_INSERT_ID();
        //    ";

        //    int horarioId = DataBase.Execute<int>(_configuration, queryMarcacao, parametersMarcacao).FirstOrDefault();

        //    var parametersAgendamento = new DynamicParameters();
        //    parametersAgendamento.Add("@usuario_id", horario.Cliente.usuarioId);
        //    parametersAgendamento.Add("@barbeiro_id", horario.BarbeiroId);
        //    parametersAgendamento.Add("@data_hora_agendamento", horario.Data.Add(horario.HoraInicio));
        //    parametersAgendamento.Add("@status", "Agendado");
        //    parametersAgendamento.Add("@horario_disponiveis_id", horarioId);

        //    var queryAgendamento = @"
        //        INSERT INTO agendamentos (usuario_id, barbeiro_id, data_hora_agendamento, status, horario_disponiveis_id) 
        //        VALUES (@usuario_id, @barbeiro_id, @data_hora_agendamento, @status, @horario_disponiveis_id);
        //    ";
        //    DataBase.Execute(_configuration, queryAgendamento, parametersAgendamento);


        //}


        //private bool VerificarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim)
        //{
        //    var query = @"
        //        SELECT COUNT(*)
        //        FROM agendamentos a
        //        JOIN marcacao_horarios mh ON a.horario_disponiveis_id = mh.horario_id
        //        WHERE a.barbeiro_id = @barbeiro_id
        //        AND DATE(a.data_hora_agendamento) = DATE(@data)
        //        AND (
        //        (@hora_inicio >= mh.hora_inicio AND @hora_inicio < mh.hora_fim)
        //        OR (@hora_fim > mh.hora_inicio AND @hora_fim <= mh.hora_fim)
        //        OR (@hora_inicio <= mh.hora_inicio AND @hora_fim >= mh.hora_fim)
        //    )";

        //    var parameters = new DynamicParameters();
        //    parameters.Add("@barbeiro_id", barbeiroId);
        //    parameters.Add("@data", data);
        //    parameters.Add("@hora_inicio", horaInicio);
        //    parameters.Add("@hora_fim", horaFim);

        //    var count = DataBase.Execute<int>(_configuration, query, parameters).FirstOrDefault();

        //    return count == 0;
        //}

        public void AtualizarHorario(HorariolMap horario)
        {
            throw new NotImplementedException();
        }

        public void DeleteHorario(int horarioId)
        {
            throw new NotImplementedException();
        }

       
    }
}

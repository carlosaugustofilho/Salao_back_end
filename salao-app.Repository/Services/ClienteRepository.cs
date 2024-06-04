
using carvao_app.Repository.Conexao;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using salao_app.Repository.Intefaces;
using salao_app.Repository.Maps;
using SalaoApp.Models;
using static Dapper.SqlMapper;

namespace salao_app.Repository.Services
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _configuration;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClienteMap BuscarClientesId(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            return DataBase.Execute<ClienteMap>(_configuration, "SELECT * FROM cliente WHERE cliente_id = @Id", parameters).FirstOrDefault();
        }

        public void CriarCliente(ClienteMap cliente)
        {
            var clientes = new DynamicParameters();
            clientes.Add("@nome", cliente.nome);
            clientes.Add("usuario_id", cliente.usuarioId);
            clientes.Add("@telefone", cliente.telefone);
            clientes.Add("@email", cliente.email);

            var query = "INSERT INTO cliente(nome, usuario_id, telefone, email) VALUES (@nome, @usuario_id, @telefone, @email)";
            DataBase.Execute(_configuration, query, clientes);
        }

        public ClienteMap BuscarClientePorId(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var query = "SELECT cliente_id clienteId , nome, email, telefone FROM cliente WHERE cliente_id = @Id";
            var cliente = DataBase.Execute<ClienteMap>(_configuration, query, parameters).FirstOrDefault();

            if (cliente != null)
            {
                return cliente;
            }
            else
            {
                throw new Exception("Cliente não encontrado");
            }
        }

        public List<ClienteMap> BuscarClientes(string nome, string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@nome", "%" + nome + "%");
            var query = "SELECT * FROM cliente_id clienteId WHERE nome LIKE @nome";

            if (!string.IsNullOrEmpty(email))
            {
                parameters.Add("@email", email);
                query += " AND email = @email";
            }

            var retorno = DataBase.Execute<ClienteMap>(_configuration, query, parameters).ToList();

            foreach (var cliente in retorno)
            {
                cliente.clienteId ??= 0;
            }

            return retorno.OrderBy(c => c.clienteId).ToList();
        }

        public void AgendarHorarioCliente(int clienteId, int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim, int usuarioId)
        {
            if (data == DateTime.MinValue)
                data = DateTime.Now.Date;

            if (data < DateTime.Now.Date)
                throw new ArgumentException("Não é possível agendar horários para datas anteriores ao dia atual.");

            if (horaInicio == TimeSpan.Zero || horaFim == TimeSpan.Zero)
                throw new ArgumentException("Horas de início e fim não podem ser nulas.");

            if (barbeiroId <= 0)
                throw new ArgumentException("Identificador do barbeiro é inválido.");

            int horarioId = VerificarHorarioDisponivel(barbeiroId, data, horaInicio, horaFim);
            if (horarioId == 0)
                throw new Exception("Não há horário disponível para este dia e hora.");

            var agendamentoparameters = new DynamicParameters();
            agendamentoparameters.Add("@usuario_id", usuarioId);
            agendamentoparameters.Add("@barbeiro_id", barbeiroId);
            agendamentoparameters.Add("@data_hora_agendamento", data.Add(horaInicio));
            agendamentoparameters.Add("@status", "Agendado");
            agendamentoparameters.Add("@horario_disponiveis_id", horarioId);

            var queryAgendamento = @"
        INSERT INTO agendamentos (usuario_id, barbeiro_id, data_hora_agendamento, status, horario_disponiveis_id) 
        VALUES (@usuario_id, @barbeiro_id, @data_hora_agendamento, @status, @horario_disponiveis_id);
    ";
            DataBase.Execute(_configuration, queryAgendamento, agendamentoparameters);

            var updateParameters = new DynamicParameters();
            updateParameters.Add("@horarioId", horarioId);

            var updateQuery = "UPDATE Horario_disponivel SET disponivel = 0 WHERE id = @horarioId";
            DataBase.Execute(_configuration, updateQuery, updateParameters);
        }


        private int VerificarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim)
        {
            var query = @"
                SELECT COUNT(*)
                FROM Horario_disponivel
                WHERE barbeiro_id = @barbeiroId
                AND data = @data
                AND hora_inicio <= @horaInicio
                AND hora_fim >= @horaFim
                AND disponivel = 1
            ";

            var parameters = new DynamicParameters();
            parameters.Add("@barbeiroId", barbeiroId);
            parameters.Add("@data", data.Date);
            parameters.Add("@horaInicio", horaInicio);
            parameters.Add("@horaFim", horaFim);

            var horarioId = DataBase.Execute<int>(_configuration, query, parameters).FirstOrDefault();
            return horarioId;
        }

        public void CancelarAgendamento(int agendamentoId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@agendamentoId", agendamentoId);

            var queryObterHorario = @"
                SELECT horario_disponiveis_id
                FROM agendamentos
                WHERE agendamento_id = @agendamentoId AND status = 'Agendado'
            ";
            var horarioId = DataBase.Execute<int>(_configuration, queryObterHorario, parameters).FirstOrDefault();

            if (horarioId == 0)
                throw new Exception("Agendamento não encontrado ou já cancelado.");

            var queryCancelarAgendamento = @"
                UPDATE agendamentos
                SET status = 'Cancelado'
                WHERE agendamento_id = @agendamentoId
            ";
            DataBase.Execute(_configuration, queryCancelarAgendamento, parameters);

            var updateParameters = new DynamicParameters();
            updateParameters.Add("@horarioId", horarioId);

            var updateQuery = "UPDATE Horario_disponivel SET disponivel = 1 WHERE id = @horarioId";
            DataBase.Execute(_configuration, updateQuery, updateParameters);
        }

        public void AtualizarCliente(ClienteMap cliente)
        {
            throw new NotImplementedException();
        }

        public void DeletarCliente(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistCliente(ClienteMap request)
        {
            var query = !string.IsNullOrEmpty(request.email) ? "Email = @Email" : "Telefone = @Telefone";

            using var connection = new MySqlConnection(_configuration["DefaultConnection"]);

            var exist = connection.QueryFirstOrDefault<ClienteMap>("SELECT * FROM cliente WHERE " + query, new
            {
                request.email,
                request.telefone
            });

            return exist != null;
        }

       
    }
}

﻿using Dapper;
using Microsoft.Extensions.Configuration;
using salao_app.Repository.Maps;
using carvao_app.Repository.Conexao; 


namespace salao_app.Repository.Services
{
    public class BarbeiroRepository : IBarbeiroRepository
    {
        private readonly IConfiguration _configuration;

        public BarbeiroRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void AdicionarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim)
        {
            var barbeiro = DataBase.Execute<BarbeiroMap>(_configuration, "SELECT * FROM barbeiros WHERE barbeiro_id = @BarbeiroId", new { BarbeiroId = barbeiroId }).FirstOrDefault();

            if (barbeiro == null)
            {
                throw new ArgumentException("Barbeiro não encontrado.");
            }

            var query = @"
        INSERT INTO Horario_disponivel (barbeiro_id, data, hora_inicio, hora_fim)
        VALUES (@BarbeiroId, @Data, @HoraInicio, @HoraFim)";

            var parameters = new DynamicParameters();
            parameters.Add("BarbeiroId", barbeiroId);
            parameters.Add("Data", data);
            parameters.Add("HoraInicio", horaInicio);
            parameters.Add("HoraFim", horaFim);

            DataBase.Execute(_configuration, query, parameters);
        }

        public List<HorarioDisponivelMap> ListarHorariosDisponiveis(int barbeiroId)
        {
            var query = @"
        SELECT 
            id AS Id, 
            barbeiro_id AS BarbeiroId, 
            data AS Data, 
            hora_inicio AS HoraInicio, 
            hora_fim AS HoraFim
        FROM 
            Horario_disponivel
        WHERE 
            barbeiro_id = @BarbeiroId";

            var parameters = new DynamicParameters();
            parameters.Add("BarbeiroId", barbeiroId);

            var result = DataBase.Execute<HorarioDisponivelMap>(_configuration, query, parameters).ToList();

            result.ForEach(h => Console.WriteLine($"ID: {h.Id}, BarbeiroId: {h.BarbeiroId}, Data: {h.Data}, HoraInicio: {h.HoraInicio}, HoraFim: {h.HoraFim}"));

            return result;
        }


        public List<HorarioDisponivelMap> ListarHorariosDisponiveisPorData(int barbeiroId, DateTime data)
        {
            var query = @"
                SELECT id, barbeiro_id, data, hora_inicio, hora_fim
                FROM Horario_disponivel
                WHERE barbeiro_id = @BarbeiroId AND data = @Data";

            var parameters = new DynamicParameters();
            parameters.Add("BarbeiroId", barbeiroId);
            parameters.Add("Data", data);

            var result = DataBase.Execute<HorarioDisponivelMap>(_configuration, query, parameters).ToList();
            
            return result;
        }




    }
}

using salao_app.Business.Interfaces;
using salao_app.Models.DTOs;
using salao_app.Models.Requests;
using salao_app.Repository.Intefaces;
using salao_app.Repository.Maps;
using SalaoApp.Models;
using System.Security.Cryptography;

namespace salao_app.Business.Services
{
    public class ClientesService : IClientesService
    {
        private readonly IClienteRepository _repository;

        public ClientesService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public void CriarCliente(ClienteRequest request)
        {
            var map = new ClienteMap
            {
                nome = request.nome,
                usuarioId = request.usuarioId,
                telefone = request.telefone,
                email = request.email,
            };

            if (ExisteCliente(map))
            {
                throw new Exception("Cliente já cadastrado!");
            }

            _repository.CriarCliente(map);
        }



        public List<ClienteDto> BuscarClientes(string nome, string email)
        {
            ClienteMap cliente = new ClienteMap();
            var resposta = _repository.BuscarClientes(nome, email);
            return new ClienteDto().ToDtoList(resposta.OrderBy(c => c.nome).ToList());
        }


        public ClienteDto BuscarClientesId(int id)
        {
            var resposta = _repository.BuscarClientePorId(id);
            if (resposta == null)
            {
                return null;
            }

            return new ClienteDto
            {
                ClienteId = resposta.clienteId ?? 0,
                Nome = resposta.nome,
                Email = resposta.email,
                Telefone = resposta.telefone
            };
        }

        public void AgendarHorarioCliente(int clienteId, int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim, int usuarioId)
        {
            _repository.AgendarHorarioCliente(clienteId, barbeiroId, data, horaInicio ,horaFim, usuarioId);
        }

        public void CancelarAgendamento(int agendamentoId)
        {
            _repository.CancelarAgendamento(agendamentoId);
        }

        public void AlterarStatusCliente(int id, bool status)
        {
            throw new NotImplementedException();
        }

        public void AtualizaDadosCliente(ClienteRequest request)
        {
            throw new NotImplementedException();
        }

        private bool ExisteCliente(ClienteMap cliente)
        {
            return _repository.ExistCliente(cliente);
        }
    }
}

using ConsultorioAPI.Data;
using ConsultorioAPI.Models;
using Microsoft.EntityFrameworkCore;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace ConsultorioAPI.Service
{
    public class PacienteService : IPacienteService
    {
        private readonly DataContext _dbContext;

        public PacienteService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ConsultaPacienteDTO>> ListarConsultasPoPaciente(int id)
        {
            var consultas = await _dbContext.Consultas 
                .Where(c => c.PacienteId == id)
                .Select(c => new ConsultaPacienteDTO
                {
                    Id = c.Id, 
                    MedicoId = c.MedicoId, 
                    DataConsulta = c.Data
                }) 
                .ToListAsync();
            return consultas;

        }

        public async Task<IEnumerable<Paciente>> ListarPacientesPorIdade(int idade)
        { 
         var ano = DateTime.Now.Year;
           var pacientes = await _dbContext.Pacientes
                .Where(p => (ano - p.DataNascimento.Year) >= idade)
                .ToListAsync();
            return pacientes;
        }
        public async Task<string> CreatePaciente(CreatePacienteDTO create)
        {
            var pacienteModel = new Paciente
            {
                Nome = create.Nome, 
                DataNascimento = create.DataNascimento, 
                CPF = create.CPF, 
                Endereco = create.Endereco, 
                Sexo = create.Sexo, 
                Telefone = create.Telefone, 
                Email = create.Email,
            }; 
            _dbContext.Add(pacienteModel); 
            await _dbContext.SaveChangesAsync();
            return "Paciente criado com sucesso";
        }
        public async Task<string> UpdatePaciente(UpdatePacienteDTO update, int id)
        {
            var paciente = await _dbContext.Pacientes.FirstOrDefaultAsync(p => p.Id == id);
            if (paciente == null) return null;
            paciente.Endereco = update.Endereco; 
            paciente.Telefone = update.Telefone;
            await _dbContext.SaveChangesAsync();
            return "Paciente atualizado com sucesso";
        }
        public async Task<string> AtualizarEndereçoPATCH(AtualizarEnderecoPATCH endereco, int id)
        {
            var paciente = await _dbContext.Pacientes.FirstOrDefaultAsync(p => p.Id == id);
            if (paciente == null) return null; 
            paciente.Endereco = endereco.Endereco;
            await _dbContext.SaveChangesAsync();
            return "Endereço atualizado com sucesso";
        }

    }

}

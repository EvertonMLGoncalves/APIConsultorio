using APIALiens.EmailModule;
using APIConsultorio.DTOs.EmailDTO;
using ConsultorioAPI.Data;
using ConsultorioAPI.Models;
using Microsoft.EntityFrameworkCore;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace ConsultorioAPI.Service
{
    public class PacienteService : IPacienteService
    {
        private readonly DataContext _dbContext;
        private readonly ISmtp _smtp;

        public PacienteService(DataContext dbContext, ISmtp smtp)
        {
            _dbContext = dbContext;
            _smtp = smtp;
        }

        public async Task<IEnumerable<ConsultaPacienteDTO>> ListarConsultasPoPacienteAsync(int id)
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

        public async Task<IEnumerable<Paciente>> ListarPacientesPorIdadeAsync(int idade)
        { 
         var ano = DateTime.Now.Year;
           var pacientes = await _dbContext.Pacientes
                .Where(p => (ano - p.DataNascimento.Year) >= idade)
                .ToListAsync();
            return pacientes;
        }

        public async Task<string> CreatePacienteAsync(CreatePacienteDTO create)
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
            try
            {
              await  _smtp.SendEmail(new EmailDTO
                {
                    To = pacienteModel.Email, 
                    Subject = "Paciente criado com sucesso", 
                    Body = "Você foi adicionado com sucesso"
                });
            } 
            catch (Exception ex)
            {
                ;
            }
            _dbContext.Add(pacienteModel); 
            await _dbContext.SaveChangesAsync();
            return "Paciente criado com sucesso";
        }

        public async Task<string> UpdatePacienteAsync(UpdatePacienteDTO update, int id)
        {
            var paciente = await _dbContext.Pacientes.FirstOrDefaultAsync(p => p.Id == id);
            if (paciente == null) return null;
            paciente.Endereco = update.Endereco; 
            paciente.Telefone = update.Telefone;
            try
            {
                await _smtp.SendEmail(new EmailDTO
                {
                    To = paciente.Email,
                    Subject = "Paciente atualizado com sucesso",
                    Body = "Seus dados foram atualizados com sucesso"
                });
            }
            catch (Exception ex)
            {
                ;
            }
            await _dbContext.SaveChangesAsync();
            return "Paciente atualizado com sucesso";
        }

        public async Task<string> AtualizarEnderecoPATCHAsync(AtualizarEnderecoPATCH endereco, int id)
        {
            var paciente = await _dbContext.Pacientes.FirstOrDefaultAsync(p => p.Id == id);
            if (paciente == null) return null; 
            paciente.Endereco = endereco.Endereco;
            try
            {
                await _smtp.SendEmail(new EmailDTO
                {
                    To = paciente.Email,
                    Subject = "Paciente atualizado com sucesso",
                    Body = "Seus dados foram atualizados com sucesso"
                });
            }
            catch (Exception ex)
            {
                ;
            }
            await _dbContext.SaveChangesAsync();
            return "Endereco atualizado com sucesso"; 

        }

        public async Task<List<PacienteDTO>> ListarTodosPacientesAsync()
        {
            var pacientes = await _dbContext.Pacientes
                .Select(p => new PacienteDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    DataNascimento = p.DataNascimento,
                    CPF = p.CPF,
                    Telefone = p.Telefone,
                    Endereco = p.Endereco,
                    Sexo = p.Sexo,
                })
                .ToListAsync();

            return pacientes;
        } 



    }

}

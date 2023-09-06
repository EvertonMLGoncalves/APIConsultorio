using APIALiens.EmailModule;
using APIConsultorio.DTOs.EmailDTO;
using ConsultorioAPI.Data;
using ConsultorioAPI.DTOs.MedicoDTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using wdwadadawdawd.DTOs.MedicoDTOs;

namespace ConsultorioAPI.Service
{
    public class MedicoService : IMedicoService
    { 
        private readonly DataContext _dataContext;
        private readonly ISmtp _smtp;

        public MedicoService(DataContext dataContext, ISmtp smtp)
        {
            _dataContext = dataContext;
            _smtp = smtp;
        }
        public async Task<IEnumerable<MedicoDTO>> ListarTodosMedicosAsync()
        {
            var medicos = await _dataContext.Medicos
            .Select(m => new MedicoDTO
            {
                Id = m.Id,
                Nome = m.Nome,
                CRM = m.CRM,
                Especialidade = m.Especialidade,
                Telefone = m.Telefone,
                Endereco = m.Endereco,
                DataNascimento = m.DataNascimento,
                Sexo = m.Sexo
            })
            .ToListAsync();

            return medicos;
        }
        public async Task<IEnumerable<ConsultaMedicoDTO>> ListarConsultarPorMedicoAsync(int id)
        {
            var consultas = await _dataContext.Consultas
                .Where(c => c.MedicoId == id)
                .Select(a => new ConsultaMedicoDTO
                {
                    Id = a.Id,
                    PacienteId = a.PacienteId,
                    DataConsulta = a.Data
                }) 
                .ToListAsync();

            return consultas;
                
        }

        public async Task<IEnumerable<Medico>> ListarMedicoPorEspecialidadeAsync(string especialidade)
        {
            var medicos = await _dataContext.Medicos 
                .Where(m => m.Especialidade.ToLower() == especialidade.ToLower()) 
                .ToListAsync();
            return medicos;
        }

        public async Task<string> CreateMedicoAsync(CreateMedicoDTO createMedico)
        {
            var medicoModel = new Medico
            {
                Nome = createMedico.Nome,
                CRM = createMedico.CRM,
                Especialidade = createMedico.Especialidade,
                Telefone = createMedico.Telefone,
                Endereco = createMedico.Endereco,
                DataNascimento = createMedico.DataNascimento,
                Sexo = createMedico.Sexo, 
                Email = createMedico.Email,
            };
             _dataContext.Medicos.Add(medicoModel); 
            await _dataContext.SaveChangesAsync();
            try
            {
                await _smtp.SendEmail(new EmailDTO
                {
                    To = medicoModel.Email,
                    Subject = "Médico criado com sucesso",
                    Body = $"Os dados do médico (a) {medicoModel.Nome} foram adicionados com sucesso."
                });
            }
            catch (Exception ex)
            {
                ;
            }
            return "Médico criado com sucesso";
        }

        public async Task<string> UpdateMedicoAsync(UpdateMedicoDTO update, int id)
        {
            var medico = await _dataContext.Medicos.FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null) return "Médico não encontrado";
            medico.Especialidade = update.Especialidade;
            medico.Telefone = update.Telefone;
            medico.Endereco = update.Endereco;
             _dataContext.Entry(medico).State = EntityState.Modified; 
            await _dataContext.SaveChangesAsync();
            try
            {
                await _smtp.SendEmail(new EmailDTO
                {
                    To = medico.Email,
                    Subject = "Médico atualizado com sucesso",
                    Body = $"A os dados do médico (a) {medico.Nome} foi atualizados com sucesso."
                });
            }
            catch (Exception ex)
            {
                ;
            }
            return "Medico atualizado com sucesso";
        }

        public async Task<string> AtualizarEspecialidadeMedicoPATCHAsync(AtualizarEspecialidadePATCH especialidade, int id)
        {
            var medico = await _dataContext.Medicos.FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null) return "Médico não encontrado";
            medico.Especialidade = especialidade.Especialidade;
            _dataContext.Entry(medico).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            try
            {
                await _smtp.SendEmail(new EmailDTO
                {
                    To = medico.Email,
                    Subject = "Médico atualizado com sucesso",
                    Body = $"A especialidade do médico (a) {medico.Nome} foi atualizada com sucesso."
                });
            }
            catch (Exception ex)
            {
                ;
            }
            return "Especialidade atualizada com sucesso";

        }

        public async Task<IEnumerable<Medico>> ListarMedicoDisponivelPorEspecialidadeEData(DateTime data, string especialidade)
        {
            var medicos = await _dataContext.Consultas
                .Include(c => c.Medico)
                .Where(c => c.Data != data && c.Medico.Especialidade.ToLower() == especialidade.ToLower())
                .Select(c => c.Medico) 
                .Distinct()
                .ToListAsync();
            var medicosComConsulta = await _dataContext.Consultas
                .Select(c => c.MedicoId)
                .Distinct()
                .ToListAsync();
            var medicosDisponiveis = await _dataContext.Medicos
                .Where(m => !medicosComConsulta.Contains(m.Id))
                .ToListAsync(); 

            return medicos.Concat(medicosDisponiveis);

        }
    }
}

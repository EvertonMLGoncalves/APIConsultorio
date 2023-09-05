using ConsultorioAPI.Data;
using ConsultorioAPI.DTOs.MedicoDTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using wdwadadawdawd.DTOs.MedicoDTOs;

namespace ConsultorioAPI.Service
{
    public class MedicoService : IMedicoService
    { 
        private readonly DataContext _dataContext;

        public MedicoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<ConsultaMedicoDTO>> ListarConsultarPorMedico(int id)
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

        public async Task<IEnumerable<Medico>> ListarMedicoPorEspecialidade(string especialidade)
        {
            var medicos = await _dataContext.Medicos 
                .Where(m => m.Especialidade.ToLower() == especialidade.ToLower()) 
                .ToListAsync();
            return medicos;
        }

        public async Task<string> CreateMedico(CreateMedicoDTO createMedico)
        {
            var medicoModel = new Medico
            {
                Nome = createMedico.Nome,
                CRM = createMedico.CRM,
                Especialidade = createMedico.Especialidade,
                Telefone = createMedico.Telefone,
                Endereço = createMedico.Endereço,
                DataNascimento = createMedico.DataNascimento,
                Sexo = createMedico.Sexo, 
                Email = createMedico.Email,
            }; 
             _dataContext.Medicos.Add(medicoModel); 
            await _dataContext.SaveChangesAsync();
            return "Médico criado com sucesso";
        }

        public async Task<string> UpdateMedico(UpdateMedicoDTO update, int id)
        {
            var medico = await _dataContext.Medicos.FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null) return "Médico não encontrado";
            medico.Especialidade = update.Especialidade;
            medico.Telefone = update.Telefone;
            medico.Endereço = update.Endereço;
             _dataContext.Entry(medico).State = EntityState.Modified; 
            await _dataContext.SaveChangesAsync();
            return "Medico atualizado com sucesso";
        }

        public async Task<string> AtualizarEspecialidadeMedicoPATCH(AtualizarEspecialidadePATCH especialidade, int id)
        {
            var medico = await _dataContext.Medicos.FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null) return "Médico não encontrado";
            medico.Especialidade = especialidade.Especialidade;
            _dataContext.Entry(medico).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return "Especialidade atualizada com sucesso";

        }
               
    }
}

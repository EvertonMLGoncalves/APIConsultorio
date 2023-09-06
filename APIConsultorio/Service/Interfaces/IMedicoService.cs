﻿using ConsultorioAPI.DTOs.MedicoDTOs;
using ConsultorioAPI.Models;
using wdwadadawdawd.DTOs.MedicoDTOs;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace ConsultorioAPI.Service.Interfaces
{
    public interface IMedicoService
    {
        Task<IEnumerable<ConsultaMedicoDTO>> ListarConsultarPorMedicoAsync(int id); 
         
        Task<IEnumerable<Medico>> ListarMedicoPorEspecialidadeAsync(string especialidade); 
        Task<string> CreateMedicoAsync(CreateMedicoDTO createMedico);
        Task<string> UpdateMedicoAsync(UpdateMedicoDTO update, int id);
        Task<string> AtualizarEspecialidadeMedicoPATCHAsync(AtualizarEspecialidadePATCH especialidade, int id);
        Task<IEnumerable<MedicoDTO>> ListarTodosMedicosAsync();
        Task<IEnumerable<Medico>> ListarMedicoDisponivelPorEspecialidadeEData(DateTime data, string especialidade);
        Task<string> DeletarMedico(int id);

    }
}

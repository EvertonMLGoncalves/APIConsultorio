using ConsultorioAPI.Models;
using System.Collections.Generic;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace ConsultorioAPI.Service
{
    public interface IPacienteService
    {

        Task<IEnumerable<ConsultaPacienteDTO>> ListarConsultasPoPacienteAsync(int id); 
        Task<IEnumerable<Paciente>> ListarPacientesPorIdadeAsync(int idade); 
        Task<string> CreatePacienteAsync(CreatePacienteDTO create);
        Task<string> UpdatePacienteAsync(UpdatePacienteDTO update, int id); 
        Task<string> AtualizarEnderecoPATCHAsync(AtualizarEnderecoPATCH endereco, int id);
        Task<List<PacienteDTO>> ListarTodosPacientesAsync();
    }
}

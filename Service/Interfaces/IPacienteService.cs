using ConsultorioAPI.Models;
using System.Collections.Generic;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace ConsultorioAPI.Service
{
    public interface IPacienteService
    {

        Task<IEnumerable<ConsultaPacienteDTO>> ListarConsultasPoPaciente(int id); 
        Task<IEnumerable<Paciente>> ListarPacientesPorIdade(int idade); 
        Task<string> CreatePaciente(CreatePacienteDTO create);
         
        Task<string> UpdatePaciente (UpdatePacienteDTO update, int id); 

        Task<string> AtualizarEndereçoPATCH(AtualizarEnderecoPATCH endereco, int id); 

    }
}

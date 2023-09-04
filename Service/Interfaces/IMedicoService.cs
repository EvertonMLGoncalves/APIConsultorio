using ConsultorioAPI.DTOs.MedicoDTOs;
using ConsultorioAPI.Models;
using wdwadadawdawd.DTOs.MedicoDTOs;

namespace ConsultorioAPI.Service.Interfaces
{
    public interface IMedicoService
    {
        Task<IEnumerable<ConsultaMedicoDTO>> ListarConsultarPorMedico(int id); 
         
        Task<string> UpdateMedico(UpdateMedicoDTO update, int id);

        Task<IEnumerable<Medico>> ListarMedicoPorEspecialidade(string especialidade); 
         
        Task<string> CreateMedico (CreateMedicoDTO createMedico);

        Task<string> AtualizarEspecialidadeMedicoPATCH(AtualizarEspecialidadePATCH especialidade, int id); 

    }
}

using ConsultorioAPI.Models;
using wdwadadawdawd.DTOs.ConsultaDTOs;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace wdwadadawdawd.Service.Interfaces
{
    public interface IConsultaService
    { 
        Task<IEnumerable<GetConsultaDTO>> GetConsultaByDataAsync(DateTime date);
        Task<Consulta> CreateConsultaAsync(AgendarConsultaDTO agendarConsultaDTO);
        Task<string> DeleteConsultaAsync(int id);
        Task<IEnumerable<ConsultaDTO>> ListarTodasConsultas();
    }
}

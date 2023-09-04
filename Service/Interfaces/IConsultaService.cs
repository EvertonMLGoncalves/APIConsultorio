using wdwadadawdawd.DTOs.ConsultaDTOs;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace wdwadadawdawd.Service.Interfaces
{
    public interface IConsultaService
    { 
        Task DeleteConsulta(int id);
        Task<string> CreateConsulta(AgendarConsultaDTO agendarConsultaDTO);
        Task<List<GetConsultaDTO>> GetConsultaByData(DateTime date);
    }
}

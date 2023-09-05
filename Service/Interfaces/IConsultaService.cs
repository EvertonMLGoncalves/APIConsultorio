using ConsultorioAPI.Models;
using wdwadadawdawd.DTOs.ConsultaDTOs;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace wdwadadawdawd.Service.Interfaces
{
    public interface IConsultaService
    { 
        Task<List<GetConsultaDTO>> GetConsultaByData(DateTime date);
        Task<Consulta> CreateConsulta(AgendarConsultaDTO agendarConsultaDTO);
        Task DeleteConsulta(int id);
    }
}

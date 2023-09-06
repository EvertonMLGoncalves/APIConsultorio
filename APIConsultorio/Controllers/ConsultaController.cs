using ConsultorioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using wdwadadawdawd.DTOs.ConsultaDTOs;
using wdwadadawdawd.DTOs.PacienteDTOs;
using wdwadadawdawd.Service.Interfaces;

namespace ConsultorioAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetConsultaDTO>>> GetConsultaByDataAsync(DateTime date)
        {
            try
            {
                var consultas = await _consultaService.GetConsultaByDataAsync(date);
                return Ok(consultas);
            }
            catch (Exception ex)
            {
                return BadRequest($"erro ao listar consultas: {ex.Message}");
            }
        } 

        [HttpPost]
        public async Task<ActionResult<Consulta>> CreateConsultaAsync(AgendarConsultaDTO agendarConsultaDTO)
        {
            try
            {
                var consultaAgendada = await _consultaService.CreateConsultaAsync(agendarConsultaDTO);
                return Ok("consulta criada");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao agendar consulta: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteConsultaAsync(int id)
        {
            try
            {
                var response = await _consultaService.DeleteConsultaAsync(id);
                if (response == null) return BadRequest("Consulta não encontrada");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"erro ao excluir consulta: {ex.Message}");
            }
        }

        
    }
}
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetConsultaByData([FromQuery] DateTime data)
        {
            try
            {
                var consultas = await _consultaService.GetConsultaByData(data);
                return Ok(consultas);
            }
            catch (Exception ex)
            {
                return BadRequest($"erro ao listar consultas: {ex.Message}");
            }
        } 

        [HttpPost]
        public async Task<IActionResult> CreateConsulta([FromBody] AgendarConsultaDTO agendarConsultaDTO)
        {
            try
            {
                var consultaAgendada = await _consultaService.CreateConsulta(agendarConsultaDTO);
                return CreatedAtAction("ObterConsultaPorId", new { id = consultaAgendada.Id}, consultaAgendada);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao agendar consulta: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            try
            {
                await _consultaService.DeleteConsulta(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"erro ao excluir consulta: {ex.Message}");
            }
        }

        
    }
}
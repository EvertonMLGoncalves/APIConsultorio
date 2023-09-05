using ConsultorioAPI.Models;
using ConsultorioAPI.Service;
using Microsoft.AspNetCore.Mvc;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }
        [HttpGet("/paciente/{id}/consultas")]
        public async Task<ActionResult<IEnumerable<ConsultaPacienteDTO>>> ListarConsultasPoPaciente(int id)
        {
            var consultas = await _pacienteService.ListarConsultasPoPaciente(id);
            if (consultas == null || !consultas.Any()) return NotFound("Paciente não encontrado");  
            return Ok(consultas);
        }

        [HttpGet("/pacientes/idade_maior_que/{idade}")]
        public async Task<ActionResult<IEnumerable<Paciente>>> ListarPacientesPorIdade(int idade)
        {
            var pacientes = await _pacienteService.ListarPacientesPorIdade(idade);
            if (pacientes == null || !pacientes.Any()) return NotFound($"Nenhum paciente tem a idade igual ou superior a {idade} anos"); 
            return Ok(pacientes);

        }

        [HttpPost("/pacientes")]
        public async Task<ActionResult<string>> CreatePaciente(CreatePacienteDTO create)
        {
            var response = await _pacienteService.CreatePaciente(create); 
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaciente([FromBody] UpdatePacienteDTO updatePaciente, int id)
        {
            try
            {
                var result = await _pacienteService.UpdatePaciente(updatePaciente, id);

                if (result == null) return NotFound("Paciente não encontrado");
                if (result == "Erro ao atualizar paciente") return BadRequest("Erro ao atualizar paciente");

                return Ok("Paciente atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar paciente: {ex.Message}");
            }
        }

        [HttpPatch("{id}/endereco")]
        public async Task<IActionResult> AtualizarEnderecoPATCH([FromBody] AtualizarEnderecoPATCH endereco, int id)
        {
            try
            {
                var result = await _pacienteService.AtualizarEndereçoPATCH(endereco, id);

                if (result == null) return NotFound("Paciente não encontrado");
                if (result == "Erro ao atualizar endereço") return BadRequest("Erro ao atualizar endereço");

                return Ok("Endereço atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar endereço: {ex.Message}");
            }
        }
    }
}
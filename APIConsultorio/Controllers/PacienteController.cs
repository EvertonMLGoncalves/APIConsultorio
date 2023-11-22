using ConsultorioAPI.Models;
using ConsultorioAPI.Service;
using Microsoft.AspNetCore.Mvc;
using wdwadadawdawd.DTOs.PacienteDTOs;

namespace ConsultorioAPI.Controllers
{
    [Route("    [controller]")]
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
            var consultas = await _pacienteService.ListarConsultasPoPacienteAsync(id);
            if (consultas == null || !consultas.Any()) return NotFound("Paciente não encontrado");  
            return Ok(consultas);
        }

        [HttpGet("/pacientes/idade_maior_que/{idade}")]
        public async Task<ActionResult<IEnumerable<Paciente>>> ListarPacientesPorIdade(int idade)
        {
            var pacientes = await _pacienteService.ListarPacientesPorIdadeAsync(idade);
            if (pacientes == null || !pacientes.Any()) return NotFound($"Nenhum paciente tem a idade igual ou superior a {idade} anos"); 
            return Ok(pacientes);

        }
        [HttpGet]
        public async Task<ActionResult> ListarTodosPacientesAsync()
        {
            try
            {
                var pacientes = await _pacienteService.ListarTodosPacientesAsync();
                return Ok(pacientes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar todos os pacientes: {ex.Message}");
            }
        }

        [HttpGet("{id}/consultas/futuras")]
        public async Task<IActionResult> ListarConsultasFuturas(int id)
        {
            try
            {
                var consultasFuturas = await _pacienteService.ListarConsultasFuturas(id);
                if (consultasFuturas == null || !consultasFuturas.Any())
                {
                    return NotFound("paciente nao tem consultas futuras.");
                }

                return Ok(consultasFuturas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar consultas futuras: {ex.Message}");
            }
        }
        [HttpPost("/pacientes")]
        public async Task<ActionResult<string>> CreatePaciente(CreatePacienteDTO create)
        {
            var response = await _pacienteService.CreatePacienteAsync(create); 
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdatePacienteAsync(UpdatePacienteDTO update, int id)
        {
            try
            {
                var result = await _pacienteService.UpdatePacienteAsync(update, id);

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
        public async Task<ActionResult<string>> AtualizarEnderecoPATCH([FromBody] AtualizarEnderecoPATCH endereco, int id)
        {
            try
            {
                var result = await _pacienteService.AtualizarEnderecoPATCHAsync(endereco, id);

                if (result == null) return NotFound("Paciente não encontrado");
                if (result == "Erro ao atualizar endereco") return BadRequest("Erro ao atualizar endereco");

                return Ok("Endereco atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar endereco: {ex.Message}");
            }
        }

        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPaciente(int id)
        {
            try
            {
                var response = await _pacienteService.DeletarPaciente(id);

                if (response == "Paciente não encontrado")
                {
                    return NotFound("Paciente não encontrado");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir paciente: {ex.Message}");
            }
        }

    }
}
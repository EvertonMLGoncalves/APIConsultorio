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
            if (consultas == null) return NotFound("Paciente não encontrado");  
            return Ok(consultas);
        }
        [HttpGet("/pacientes/idade_maior_que/{idade}")]
        public async Task<ActionResult<IEnumerable<Paciente>>> ListarPacientesPorIdade(int idade)
        {
            var pacientes = await _pacienteService.ListarPacientesPorIdade(idade);
            if (pacientes == null) return NotFound($"Nenhum paciente tem a idade igual ou superior a {idade} anos"); 
            return Ok(pacientes);

        }
        [HttpPost("/pacientes")]
        public async Task<ActionResult<string>> CreatePaciente(CreatePacienteDTO create)
        {
            var response = await _pacienteService.CreatePaciente(create); 
            return Ok(response);
        }




    }
}
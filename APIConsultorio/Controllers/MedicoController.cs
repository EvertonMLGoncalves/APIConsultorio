using ConsultorioAPI.DTOs.MedicoDTOs;
using ConsultorioAPI.Models;
using ConsultorioAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using wdwadadawdawd.DTOs.MedicoDTOs;

namespace ConsultorioAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicicoService)
        {
            _medicoService = medicicoService;
        }

        [HttpGet("{id}/consultas")]
        public async Task<ActionResult<IEnumerable<ConsultaMedicoDTO>>> ListarConsultasPorMedico(int id)
        {
            try
            {
                var consultas = await _medicoService.ListarConsultarPorMedicoAsync(id);

                if (consultas == null || !consultas.Any())
                {
                    return NotFound("Medico nao encontrado ou nao possui consultas.");
                }

                return Ok(consultas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar consultas do medico:\n {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medico>>> ListarMedicoPorEspecialidade(string especialidade)
        {
            try
            {
                var consultas = await _medicoService.ListarMedicoPorEspecialidadeAsync(especialidade);

                if (consultas == null || !consultas.Any())
                {
                    return NotFound("Medico nao encontrado na especialidade.");
                }

                return Ok(consultas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar medico na especialidade:\n {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateMedicoAsync(CreateMedicoDTO createMedico)
        {
            try
            {
                 var response = await _medicoService.CreateMedicoAsync(createMedico);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar medico:\n {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateMedicoAsync(UpdateMedicoDTO update, int id)
        {
            try
            {
                var result = await _medicoService.UpdateMedicoAsync(update, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar o medico:\n {ex.Message}");
            }
        }

        [HttpPatch("/medicos/{id}")]
        public async Task<ActionResult<string>> AtualizarEspecialidadeMedicoPATCH([FromBody] AtualizarEspecialidadePATCH especialidade, int id)
        {
            try
            {
                var result = await _medicoService.AtualizarEspecialidadeMedicoPATCHAsync(especialidade, id);
                return Ok(result);
            } 
            catch(Exception ex)
            {
                return BadRequest($"Erro ao atualizar a especialidade do medico:\n {ex.Message}");
            }
        }

        [HttpGet("/medicos")]
        public async Task<ActionResult> ListarTodosMedicosAsync()
        {
            try
            {
                var medicos = await _medicoService.ListarTodosMedicosAsync();
                return Ok(medicos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar médicos: {ex.Message}");
            }
        }
    }

}
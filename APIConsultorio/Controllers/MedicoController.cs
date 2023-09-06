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
        [HttpGet("/medicos")]
        public async Task<ActionResult<IEnumerable<MedicoDTO>>> ListarTodosMedicosAsync()
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

        [HttpGet("/medicos/especialidade/{especialidade}")]
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

        [HttpGet("medicos/disponiveis")]
        public async Task<ActionResult<IEnumerable<Medico>>> ListarMedicoDisponivelPorEspecialidadeEData([FromQuery] DateTime data, [FromQuery] string especialidade)
        {
            try
            {
                var medicos = await _medicoService.ListarMedicoDisponivelPorEspecialidadeEData(data, especialidade);
                if (!medicos.Any()) return BadRequest("Nenhum médico disponível para essa data com essa especialidade");
                return Ok(medicos);
            } 
            catch(Exception ex)
            {
                return BadRequest($"Erro ao buscar médicos por data e especialidade:\n {ex.Message}");
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarMedico(int id)
        {
            try
            {
                var response = await _medicoService.DeletarMedico(id);

                if (response == "Medico não encontrado")
                {
                    return NotFound("Medico não encontrado");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir medico: {ex.Message}");
            }
        }

    }

}
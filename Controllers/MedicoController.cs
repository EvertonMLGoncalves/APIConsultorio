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
        public async Task<IActionResult> ListarConsultasPorMedico(int id)
        {
            try
            {
                var consultas = await _medicoService.ListarConsultarPorMedico(id);

                if (consultas == null)
                {
                    return NotFound("Medico nao encontrado ou nao possui consultas.");
                }

                return Ok(consultas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar consultas do medico: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarMedicoPorEspecialidade([FromQuery] string especialidade)
        {
            try
            {
                var consultas = await _medicoService.ListarMedicoPorEspecialidade(especialidade);

                if (consultas == null)
                {
                    return NotFound("Medico nao encontrado na especialidade.");
                }

                return Ok(consultas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar medico na especialidade: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedico([FromBody] CreateMedicoDTO createMedico)
        {
            try
            {
                await _medicoService.CreateMedico(createMedico);
                return Ok("Medico criado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar medico: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedico([FromBody] UpdateMedicoDTO update, int id)
        {
            try
            {
                var result = await _medicoService.UpdateMedico(id, update);
            }
        }
    }

}
﻿using ConsultorioAPI.Data;
using ConsultorioAPI.Models;
using Microsoft.EntityFrameworkCore;
using wdwadadawdawd.DTOs.ConsultaDTOs;
using wdwadadawdawd.DTOs.PacienteDTOs;
using wdwadadawdawd.Service.Interfaces;

namespace wdwadadawdawd.Service
{
    public class ConsultaService : IConsultaService
    {
        private readonly DataContext _dbContext; 


        public ConsultaService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GetConsultaDTO>> GetConsultaByDataAsync(DateTime date)
        {
            var consultas = await _dbContext.Consultas
                .Where(c => c.Data.Date == date.Date)
                .Select(c => new GetConsultaDTO
                {
                    Id = c.Id,
                    Data = c.Data,
                    Descricao = c.Descricao,
                    Prescricao = c.Prescricao, 
                    MedicoId = c.MedicoId, 
                    PacienteId = c.PacienteId,
                })
                .ToListAsync();

            return consultas;
        }

        public async Task<Consulta> CreateConsultaAsync(AgendarConsultaDTO agendarConsultaDTO)
        {
            var medico = await _dbContext.Medicos.FindAsync(agendarConsultaDTO.MedicoId);
            var paciente = await _dbContext.Pacientes.FindAsync(agendarConsultaDTO.PacienteId);

            if (paciente == null || medico == null)
            {
                throw new Exception("medico ou paciente nao encontrado.");
            }
            var novaConsulta = new Consulta
            {
                Medico = medico,
                Paciente = paciente,
                Data = agendarConsultaDTO.DataConsulta,
                Descricao = agendarConsultaDTO.Descricao,
                Prescricao = agendarConsultaDTO.PrescricaoMedica
            }; 

            _dbContext.Consultas.Add(novaConsulta); 

            await _dbContext.SaveChangesAsync(); 

            return novaConsulta;
        }

        public async Task<string> DeleteConsultaAsync(int id)
        {
            var consulta = await _dbContext.Consultas.FindAsync(id);

            if (consulta == null) return null;

            _dbContext.Consultas.Remove(consulta);
            await _dbContext.SaveChangesAsync();
            return "Consulta deletada com sucesso";
        }

        public async Task<IEnumerable<ConsultaDTO>> ListarTodasConsultas()
        {
            var consultas = await _dbContext.Consultas
                .Select(c => new ConsultaDTO
                {
                    Id = c.Id,
                    DataConsulta = c.Data,
                    Descricao = c.Descricao,
                    Prescricao = c.Prescricao,
                })
                .ToListAsync();

            return consultas;
        }
    }
}

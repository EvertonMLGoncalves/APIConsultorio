namespace wdwadadawdawd.DTOs.PacienteDTOs
{
    public class ConsultaDTO
    {
        public int Id { get; set; }
        public DateTime DataConsulta { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Prescricao { get; set; } = string.Empty;
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
    }
}
namespace wdwadadawdawd.DTOs.PacienteDTOs
{
    public class AgendarConsultaDTO
    {
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataConsulta { get; set; }
        public string Descricao { get; set; }
        public string PrescricaoMedica { get; set; }

    }
}
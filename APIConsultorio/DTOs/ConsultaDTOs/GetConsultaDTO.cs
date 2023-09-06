namespace wdwadadawdawd.DTOs.ConsultaDTOs
{
    public class GetConsultaDTO
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }

        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Prescricao { get; set; }
    }
}

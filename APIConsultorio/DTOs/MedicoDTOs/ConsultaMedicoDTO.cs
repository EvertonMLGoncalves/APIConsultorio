namespace ConsultorioAPI.DTOs.MedicoDTOs
{
    public class ConsultaMedicoDTO
    { 
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataConsulta { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsultorioAPI.Models
{
    public class Consulta
    {
       
        public int Id { get; set; } 
        public int MedicoId { get; set; } 
        public int PacienteId { get; set; } 
   
        public DateTime Data { get; set; } 
         
        public string Descricao { get; set; } = string.Empty; 
        public string Prescricao { get; set; } = string.Empty;


        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }


    }
}

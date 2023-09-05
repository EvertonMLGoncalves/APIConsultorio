
using APIConsultorio.DTOs.EmailDTO;

namespace APIALiens.EmailModule
{
    public interface ISmtp
    { 
        Task<string> SendEmail(EmailDTO emailDTO);
    }
}

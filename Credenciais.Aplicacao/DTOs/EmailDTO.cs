using System.ComponentModel.DataAnnotations;

namespace Credenciais.Aplicacao.DTOs
{
    public class EmailDTO
    {
        [Required, Display(Name = "Email de destino"), EmailAddress]
        public string EmailDestino { get; set; }

        [Required, Display(Name = "Assunto")]
        public string Assunto { get; set; }

        [Required, Display(Name = "Mensagem")]
        public string Mensagem { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Credenciais.Aplicacao.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}

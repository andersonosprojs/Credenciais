using System.ComponentModel.DataAnnotations;

namespace Credenciais.Aplicacao.DTOs
{
    public  class UsuarioDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Login é obrigatório")]
        [MaxLength(300)]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MaxLength(300)]
        public string? Senha { get; set; }
    }
}

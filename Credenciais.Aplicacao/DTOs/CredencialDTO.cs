using System.ComponentModel.DataAnnotations;

namespace Credenciais.Aplicacao.DTOs
{
    public class CredencialDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Titulo é obrigatório")]
        [MaxLength(300)]        
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "Login é obrigatório")]
        [MaxLength(300)]        
        public string? Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MaxLength(300)]
        public string? Senha { get; set; }

        [MaxLength(50)]
        public string? Assinatura { get; set; }

        [MaxLength(1000)]
        public string? Url { get; set; }

        [MaxLength(50)]
        public string? Agencia { get; set; }

        [MaxLength(50)]
        public string? Conta { get; set; }

        [MaxLength(1000)]
        public string? Pix { get; set; }

        [MaxLength(300)]
        public string? UsuarioAplicativo { get; set; }

        [MaxLength(300)]
        public string? SenhaAplicativo { get; set; }

        [MaxLength(300)]
        public string? SenhaCartao { get; set; }

        [MaxLength(1000)]
        public string? Observacao { get; set; }
        public long IdUsuario { get; set; }

    }
}

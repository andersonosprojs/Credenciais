namespace Credenciais.Aplicacao.DTOs
{
    public class TokenDTO
    {
        public string? Token { get; set; }
        public DateTime Expiracao { get; set; }
        public string? Login { get; set; }
        public long IdUsuario { get; set; }
    }
}

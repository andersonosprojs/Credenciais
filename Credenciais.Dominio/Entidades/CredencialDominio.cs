using Credenciais.Dominio.Validacao;

namespace Credenciais.Dominio.Entidades
{
    public class CredencialDominio: EntidadeBase
    {
        public CredencialDominio(string? titulo, string? login, string? senha, string? assinatura, 
            string? url, string? agencia, string? conta, string? pix, string? usuarioAplicativo, 
            string? senhaAplicativo, string? senhaCartao, string? observacao)
            => ValidaDominio(titulo, login, senha, assinatura, url, agencia, conta, pix, usuarioAplicativo,
                senhaAplicativo, senhaCartao, observacao);

        public CredencialDominio(long id, string? titulo, string? login, string? senha, string? assinatura,
            string? url, string? agencia, string? conta, string? pix, string? usuarioAplicativo,
            string? senhaAplicativo, string? senhaCartao, string? observacao)
        {
            ValidacaoDominio.When(id < 0, "Indentificador Inválido");
            Id = id;
            ValidaDominio(titulo, login, senha, assinatura, url, agencia, conta, pix, usuarioAplicativo,
                senhaAplicativo, senhaCartao, observacao);
        }

        public string? Titulo { get; set; }
        public string? Login { get; private set; }
        public string? Senha { get; private set; }
        public string? Assinatura { get; private set; }
        public string? Url { get; private set; }
        public string? Agencia { get; private set; }
        public string? Conta { get; private set; }
        public string? Pix { get; private set; }
        public string? UsuarioAplicativo { get; private set; }
        public string? SenhaAplicativo { get; private set; }
        public string? SenhaCartao { get; private set; }
        public string? Observacao { get; private set; }
        public long IdUsuario { get; set; }
        public UsuarioDominio Usuario { get; set; }


        private void ValidaDominio(string? titulo, string? login, string? senha, string? assinatura,
            string? url, string? agencia, string? conta, string? pix, string? usuarioAplicativo,
            string? senhaAplicativo, string? senhaCartao, string? observacao)
        {
            ValidacaoDominio.When(string.IsNullOrEmpty(titulo),
                "Título é obrigatório");

            ValidacaoDominio.When(string.IsNullOrEmpty(login),
                "Login é obrigatório");

            ValidacaoDominio.When(string.IsNullOrEmpty(senha),
                "Senha é obrigatório");

            Titulo = titulo;
            Login = login;
            Senha = senha;
            Assinatura = assinatura;
            Url = url;
            Agencia = agencia;
            Conta = conta;  
            Pix = pix;
            UsuarioAplicativo = usuarioAplicativo;
            SenhaAplicativo = senhaAplicativo;
            SenhaCartao = senhaCartao;
            Observacao = observacao;
        }
    }
}

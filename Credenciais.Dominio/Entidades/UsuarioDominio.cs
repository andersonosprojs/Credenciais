using Credenciais.Dominio.Validacao;

namespace Credenciais.Dominio.Entidades
{
    public class UsuarioDominio: EntidadeBase
    {
        public UsuarioDominio(string? login, string? senha)
            => ValidaDominio(login, senha);

        public UsuarioDominio(long id, string? login, string? senha)
        {
            ValidacaoDominio.When(id < 0, "Indentificador Inválido");
            Id = id;
            ValidaDominio(login, senha);
        }

        public string? Login { get; private set; }
        public string? Senha { get; private set; }
        public ICollection<CredencialDominio> Credenciais { get; set; }

        public void setSenha(string? senha) => this.Senha = senha;

        private void ValidaDominio(string? login, string? senha)
        {
            ValidacaoDominio.When(string.IsNullOrEmpty(login),
                "Login é obrigatório");

            ValidacaoDominio.When(string.IsNullOrEmpty(senha),
                "Senha é obrigatório");

            Login = login;
            Senha = senha;
        }
    }
}

namespace Credenciais.Dominio.Validacao;

public class ValidacaoDominio : Exception
{
    public ValidacaoDominio(string erro) : base(erro)
    { }

    public static void When(bool temErro, string erro)
    {
        if (temErro)
            throw new ValidacaoDominio(erro);
    }
}

namespace Credenciais.Utils.Interfaces
{
    public  interface ICriptografiaServico
    {
        byte[] ObterChave();
        string? Criptografar(string? texto);
        string? Descriptografar(string? texto);
        string CriptografarUrl(string texto);
        string DescriptografarUrl(string texto);
    }
}

using Credenciais.Utils.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Credenciais.Utils.Servicos
{
    public class CriptografiaServico: ICriptografiaServico
    {

        private string _chave;
        private SymmetricAlgorithm _algoritmo;

        public CriptografiaServico()
            => _algoritmo = new RijndaelManaged() { Mode = CipherMode.CBC };

        public byte[] ObterChave()
        {
            _chave = "credenciais_#2022@12";

            if (_algoritmo.LegalKeySizes.Length > 0)
            {
                int tamChave = _chave.Length * 8;
                int tamMin = _algoritmo.LegalKeySizes[0].MinSize;
                int tamMax = _algoritmo.LegalKeySizes[0].MaxSize;
                int pularTam = _algoritmo.LegalKeySizes[0].SkipSize;

                if (tamChave > tamMax)
                    _chave = _chave.Substring(0, tamMax / 8);
                else
                {
                    int tamValido = tamChave <= tamMin ? tamMin : tamChave - tamChave % pularTam + pularTam;
                    if (tamChave < tamValido)
                        _chave = _chave.PadRight(tamValido / 8, '*');
                }
            }
            PasswordDeriveBytes chave = new PasswordDeriveBytes(_chave, Encoding.ASCII.GetBytes(string.Empty));
            return chave.GetBytes(_chave.Length);
        }

        public string? Criptografar(string? texto)
        {
            try
            {
                byte[] textByte = Encoding.UTF8.GetBytes(texto);
                byte[] chaveByte = ObterChave();

                _algoritmo.Key = chaveByte;
                _algoritmo.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };


                ICryptoTransform cryptoTransform = _algoritmo.CreateEncryptor();
                MemoryStream _memoryStream = new MemoryStream();
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);

                _cryptoStream.Write(textByte, 0, textByte.Length);
                _cryptoStream.FlushFinalBlock();

                byte[] cryptoByte = _memoryStream.ToArray();

                return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
            }
            catch
            {
                return texto;
            }
        }

        public string? Descriptografar(string? texto)
        {
            try
            {
                byte[] textoByte = Convert.FromBase64String(texto);
                byte[] chaveByte = ObterChave();

                _algoritmo.Key = chaveByte;
                _algoritmo.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };


                ICryptoTransform cryptoTransform = _algoritmo.CreateDecryptor();
                MemoryStream _memoryStream = new MemoryStream(textoByte, 0, textoByte.Length);
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);
                StreamReader _streamReader = new StreamReader(_cryptoStream);
                return _streamReader.ReadToEnd();
            }
            catch
            {
                return texto;
            }
        }

        public string CriptografarUrl(string texto)
        {
            var preCodificacao = Encoding.UTF8.GetBytes(texto);
            var codificado = HttpUtility.UrlEncode(Convert.ToBase64String(preCodificacao));
            return codificado;
        }

        public string DescriptografarUrl(string texto)
        {
            var preDecodificado = Convert.FromBase64String(HttpUtility.UrlDecode(texto));
            var decodificado = Encoding.UTF8.GetString(preDecodificado);
            return decodificado;
        }
    }
}
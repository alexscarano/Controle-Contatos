using System.Security.Cryptography;
using System.Text;

namespace ControleContatos.Helpers
{
    public static class Criptografia
    {
        public static string GerarHash(this string senha)
        {
            var hash = SHA256.Create();
            var encoding = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(senha);

            bytes = hash.ComputeHash(bytes);

            var strHexa = new StringBuilder();

            foreach (byte b in bytes)
            {
                strHexa.Append(b.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}

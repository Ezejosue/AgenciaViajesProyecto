using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text;


namespace AgenciaViajes.Recursos
{
    public class Util
    {

        public static string encriptarClave(string clave)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(clave));

                foreach (byte b in result)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

            }

            return stringBuilder.ToString();
        }
    }
}

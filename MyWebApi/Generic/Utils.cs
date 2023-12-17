using System.Security.Cryptography;
using System.Text;

namespace MyWebApi.Generic
{
    public class Utils
    {
        public static string CifrarCadena(string cadena)
        {
            SHA256Managed sha = new SHA256Managed();
            byte[] bytecadena = Encoding.Default.GetBytes(cadena);
            byte[] bytecifrado = sha.ComputeHash(bytecadena);
            return BitConverter.ToString(bytecifrado).Replace("-", "");
        }
    }
}

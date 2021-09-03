using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace MediApp.Services
{
    public static class MethodService
    {
        public static void AlertInvalidation(string s)
        {
            Shell.Current.DisplayAlert("Entrada inválida", s, "Ok");
        }

        public static string GetHash(string p)
        {
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(p);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            StringBuilder stringBuilder = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length; i++)
            {
                stringBuilder.Append(tmpHash[i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}

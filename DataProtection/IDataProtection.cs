using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.DataProtection
{
    public interface IDataProtection
    {
        string Protect(string text);    // Girilen metni şifreli bir metin haline dönüştürecek olan metotç
        string UnProtect(string protectedText); // Gönderilen şifreli metni açacak bir metot.
    }
}

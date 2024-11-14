using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.DataProtection
{
    // IDATAPROTECTION İÇERİSİNDEKİ METOTLARIN İÇİ DOLDURULUR...
    public class DataProtection : IDataProtection
    {
        private readonly IDataProtector _protector;   // Kütüphanenin içinde olan class implement edildi.

        public DataProtection(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("security");
        }
        public string Protect(string text)
        {
            return _protector.Protect(text);    // Metin şifrelendi.
        }

        public string UnProtect(string protectedText)
        {
           return _protector.Unprotect(protectedText);  // Şifrelenen metin açıldı.
        }
    }
}

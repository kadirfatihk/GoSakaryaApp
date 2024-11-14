using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Data.Enums
{
    public enum UserType
    {
        Admin,  // admin-> gezilecek yer ve etkinlik ekleme... güncelleme... silme... jwt ile giriş...
        Visitor     // ziyaretçi-> sadece get ile listeleme... belki yorum ve rating
    }
}

using GoSakaryaApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.User.Dtos
{
    // GİRİŞ İŞLEMİ BAŞARIYLA TAMAMLANDIKTAN SONRA TOKEN OLUŞTURMAK İÇİN KULLANILACAK DTO
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
    }
}

using GoSakaryaApp.Business.DataProtection;
using GoSakaryaApp.Business.Operations.User.Dtos;
using GoSakaryaApp.Business.Types;
using GoSakaryaApp.Data.Entities;
using GoSakaryaApp.Data.Enums;
using GoSakaryaApp.Data.Repositories;
using GoSakaryaApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.User
{
    // SERVICE'TE TANIMLANAN METOTLARIN İÇİ DOLDURULUR...
    public class UserManager : IUserService
    {
        // DEPENDENCY INJECTION İÇİNDEKİ METOTLAR ÇEKİLDİ.
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _dataProtection;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection dataProtection)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _dataProtection = dataProtection;
        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            // Email ve şifre kontrolü yapılır.
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());
            var hasPassword = _userRepository.GetAll(x => x.Password.ToLower() == user.Password.ToLower());

            // Email kullanımdaysa hata mesajı döndürülür.
            if (hasMail.Any())
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu email adresi kullanımda. Lütfen başka bir mail adresi belirleyiniz."
                };

            // Yeni kullanıcı entity'si oluşturulur.
            var userEntity = new UserEntity    // DTO İÇİNDEKİ VERİLER ENTITY'E ÇEVRİLDİ. ÇÜNKÜ REPOSİTORY'DE ADD METODU ENTİTY PARAMETRESİ ALIYOR...
            {
                Email = user.Email,
                Password = _dataProtection.Protect(user.Password),  // Data Protector ile şifre koruma altına alınır.
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserType = UserType.Visitor,
            };

            // Kullanıcı veritabanına eklenir.
            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();   // Değişiklikler veritabanına kaydedilir.
            }
            catch (Exception)    // Hata durumunda exception fırlatılır.
            {
                throw new Exception("Kullanıcı kaydı sırasında bir hatayla karşılaşıldı.");
            }

            return new ServiceMessage   // Başarılı olursa mesaj döndürülür..
            {
                IsSucceed = true,
                Message = "Kullanıcı kaydı başarıyla oluşturuldu."
            };
        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            // Emai ile kullanıcı aranır.
            var userEntity = _userRepository.Get(x=>x.Email == user.Email);

            // Kullanıcı bulunamazsa hata mesajı döndürülür.
            if (userEntity is null)
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı."
                };
            
            var unprotectedPassword = _dataProtection.UnProtect(userEntity.Password);   // Unprotect metodu ile şifre çözülür.

            // Şifreler eşleşirse giriş sağlanır.
            if (unprotectedPassword == user.Password)
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Message = "Giriş başarılı.",
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName= userEntity.LastName,
                        UserType = userEntity.UserType,
                    }
                };
            else
                return new ServiceMessage<UserInfoDto>  // Şifreler eşleşmiyorsa hata mesajı döndürülür.
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı."
                };
            
        }
    }
}

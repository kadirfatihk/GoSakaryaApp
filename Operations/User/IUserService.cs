using GoSakaryaApp.Business.Operations.User.Dtos;
using GoSakaryaApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.User
{
    // METOTLAR TANIMLANIR... USERMANAGER'DA KULLANILIR...
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);  // Async çünkü UnitOfWork kullanılacak.
        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}

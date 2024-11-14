using GoSakaryaApp.Business.Operations.User;
using GoSakaryaApp.Business.Operations.User.Dtos;
using GoSakaryaApp.WebApi.Jwt;
using GoSakaryaApp.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSakaryaApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestModel requestModel)
        {
            // Gelen isteğin modelinin geçerliliğini kontrol eder. Eğer model geçerli değilse (örneğin, gerekli alanlar boşsa) BadRequest döner.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // RegisterRequestModel'den AddUserDto'ya verileri eşler.
            var addUserDto = new AddUserDto    
            {
                Email = requestModel.Email,
                Password = requestModel.Password,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                PhoneNumber = requestModel.PhoneNumber,
            };

            // _userService.AddUser metodunu asenkron olarak çağırır.
            // Bu metot muhtemelen veritabanına kullanıcı ekleme işlemini gerçekleştirir ve bir ServiceMessage nesnesi döndürür.
            var result = await _userService.AddUser(addUserDto);  

            if(result.IsSucceed)
                return Ok(result.Message);     
            else
                return BadRequest(result.Message);  
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestModel requestModel)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.LoginUser(new LoginUserDto { Email = requestModel.Email, Password = requestModel.Password });

            if(!result.IsSucceed)
                return BadRequest(result.Message);

            var user = result.Data;

            // Property Injection
            var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName= user.LastName,
                UserType = user.UserType,
                SecretKey = config["Jwt:SecretKey"]!,
                Issuer = config["Jwt:Issuer"]!,
                Audience = config["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(config["Jwt:ExpireMinutes"]!)
            });

            return Ok(new LoginResponseModel
            {
                Message = "Giriş başarılı.",
                Token = token,
            });
        }
    }


}

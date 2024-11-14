using GoSakaryaApp.Business.Operations.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSakaryaApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        // Bakım modunu değiştirmek (açma/kapama) için HTTP PATCH metodu.
        [HttpPatch]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ToggleMaintenance()
        {
            await _settingService.ToggleMaintenance();      // Bakım modunu değiştirir.

            return Ok();
        }
    }
}

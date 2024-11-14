using GoSakaryaApp.Business.Operations.Setting;

namespace GoSakaryaApp.WebApi.Middlewares
{
    public class MaintenanceModeMiddleware
    {
        private readonly RequestDelegate _next;
       

        public MaintenanceModeMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            // Ayar servisini alır.
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();

            // Bakım modu durumunu alır.
            bool maintenanceMode = settingService.GetMaintenanceState();

            // Ayarlar ve giriş endpoint'lerini bakım modundan hariç tutar.
            if (context.Request.Path.StartsWithSegments("/api/settings") || context.Request.Path.StartsWithSegments("/api/auth/login"))
            {
                await _next(context);       // Sonraki middleware'e geçer.
                return;     // Metodu sonlandırır.
            }

            // Bakım modu açıksa bakım mesajı döndürür.
            if (maintenanceMode)
                await context.Response.WriteAsync("Sizlere daha iyi hizmet verebilmek için bakımdayız...");
            // Bakım modu kapalı ise sonraki middleware'e geçer.
            else
                await _next(context);
        }
    }
}

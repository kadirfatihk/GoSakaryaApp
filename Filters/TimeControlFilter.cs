using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoSakaryaApp.WebApi.Filters
{
    // Aksiyon metotlarının belirli saatler arasında çalışmasını engelleyen bir Action Filter.
    public class TimeControlFilter : ActionFilterAttribute
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var now = DateTime.Now.TimeOfDay;   // Güncel sat kontrolü.

            StartTime = "09:00";
            EndTime = "23:00";

            if (now >= TimeSpan.Parse(StartTime) && now <= TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuting(context);
            }

            else
            {
                context.Result = new ContentResult
                {
                    Content = "Bu saatler arasında Konum veya Etkinlik ekleme işlemi yapılamaz.",
                    StatusCode = 403
                };
            }
        }
    }
}

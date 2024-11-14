using System.ComponentModel.DataAnnotations;

namespace GoSakaryaApp.WebApi.Models
{
    public class UpdateEventRequestModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public  string Location { get; set; }
        public string? Artist { get; set; }
        public string? TicketPrice { get; set; }    // bilet fiyatı

        [Required]
        public string Description { get; set; }
        public int TicketCapacity { get; set; }

        [Required]
        public DateTime EventDate { get; set; }     // etkinlik tarihi

        [Required]
        public string EventDuration { get; set; }  // etkinlik süresi(kaç saat)
    }
}

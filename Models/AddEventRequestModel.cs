using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace GoSakaryaApp.WebApi.Models
{
    public class AddEventRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public string? Artist { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string TicketPrice { get; set; }   

        [Required]
        public DateTime EventDate { get; set; }    

        [Required]
        public string EventDuration { get; set; }

        [Required(ErrorMessage = "Bilet kapasitesi gereklidir.")]
        [Range(1, int.MaxValue, ErrorMessage = "Bilet kapasitesi en az 1 olmalıdır.")]
        public int TicketCapacity { get; set; }
    }
}

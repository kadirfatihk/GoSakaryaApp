using System.ComponentModel.DataAnnotations;

namespace GoSakaryaApp.WebApi.Models
{
    public class BuyTicketRequestModel
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bilet sayısı 1 olmalıdır.")]
        [Required]
        public int TicketCount { get; set; }
    }
}

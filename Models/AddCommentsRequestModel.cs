using System.ComponentModel.DataAnnotations;

namespace GoSakaryaApp.WebApi.Models
{
    public class AddCommentsRequestModel
    {
        [Required]
        public string Text { get; set; }
        public int? AreaId { get; set; }
        public int? EventId { get; set; }
    }
}

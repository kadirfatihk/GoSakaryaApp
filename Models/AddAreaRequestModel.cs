using GoSakaryaApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoSakaryaApp.WebApi.Models
{
    public class AddAreaRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string History { get; set; }
    }
}

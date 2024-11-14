using System.ComponentModel.DataAnnotations;

namespace GoSakaryaApp.WebApi.Models
{
    public class UpdateAreaRequestModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string History { get; set; }
    }
}

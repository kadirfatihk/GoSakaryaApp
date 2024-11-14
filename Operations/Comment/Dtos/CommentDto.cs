using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.Comment.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? AreaId { get; set; }
        public int? EventId { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? EventName { get; set; }  // *****
        public string? AreaName { get; set; }   // *****
    }
}

using GoSakaryaApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.Area.Dtos
{
    // Konum eklemesi yapılırken kullanılan Dto
    public class AddAreaDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string History { get; set; }
    }
}

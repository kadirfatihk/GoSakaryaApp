using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.Event.Dtos
{
    // ETKİNLİK EKLEME DTO
    public class AddEventDto
    {   
        public string Name { get; set; }       
        public string Location { get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
        
        public string? TicketPrice { get; set; }        
        public DateTime EventDate { get; set; }
        public string EventDuration { get; set; }
        public int TicketCapacity { get; set; }
    }
}

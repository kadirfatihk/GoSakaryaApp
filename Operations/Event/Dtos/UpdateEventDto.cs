using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.Event.Dtos
{
    public class UpdateEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string? Artist { get; set; }
        public string? TicketPrice { get; set; }    // bilet fiyatı
        public string Description { get; set; }
        public int TicketCapacity { get; set; }
        public DateTime EventDate { get; set; }     // etkinlik tarihi
        public string EventDuration { get; set; }  // etkinlik süresi(kaç saat)
    }
}

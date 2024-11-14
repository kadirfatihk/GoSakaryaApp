using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.EvenTicket.Dtos
{
    public class BuyTicketDto
    {
        public int EventId { get; set; }
        public int TicketCount { get; set; }
        public int UserId { get; set; }
    }
}

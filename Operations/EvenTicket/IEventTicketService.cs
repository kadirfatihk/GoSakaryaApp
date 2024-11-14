using GoSakaryaApp.Business.Operations.Event.Dtos;
using GoSakaryaApp.Business.Operations.EvenTicket.Dtos;
using GoSakaryaApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.EvenTicket
{
    public interface IEventTicketService
    {
        Task<ServiceMessage> BuyTicket(BuyTicketDto buyTicketDto);
        Task<ServiceMessage<List<EventDto>>> GetTicketByUser(int userId);
    }
}
